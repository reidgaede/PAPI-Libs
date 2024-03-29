/* Code in this file principally handles interaction between "PAPI_LibController.cs" and the database context.
File contents should be rather standard with exception of contents of the `Add()` and `Update()` methods, which 
are highly customized and constitute the vast majority of this file's code. */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Text.Json;
using PAPI_Libs.Models;
using System.Security.Cryptography.X509Certificates;
using PAPI_Libs.Data;
using Microsoft.EntityFrameworkCore;

namespace PAPI_Libs.Services;

public class PAPI_LibService
{
    private readonly PAPI_LibContext _context;

    public PAPI_LibService(PAPI_LibContext context)
    {
        _context = context;
    }

    public IEnumerable<PAPI_Lib> GetAll()
    {
        return _context.PAPI_Libs
        .AsNoTracking()
        .ToList();
    }

    public PAPI_Lib? GetById(int id)
    {
        return _context.PAPI_Libs
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public async Task Add(PAPI_Lib papi_lib)
    {
        Random numberGenerator = new Random();

        /* Randomly generating an `Id` value with which to obtain a `PAPI_LibTemplate` from the 
        "PAPI_LibTemplates" table in our database context: */
        int selectedTemplateId = numberGenerator.Next(1, 6);

        /* Number of `switch()` cases corresponds exactly with number of unique `PAPI_LibTemplate` objects pre-populated 
        in "PAPI-Libs.db" when app is built/run for first time: */
        switch(selectedTemplateId)
        {
            case 1:
                PAPI_LibTemplate selectedTemplate1 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                /* For Fruityvice API, 64 to 72 was longest stretch of consecutive, `GET`-able objects on basis of ID value: */
                int selectedFruitId = numberGenerator.Next(64, 73);

                /* Using `String.Format()` to insert/interpolate an ID value into the URL used to make a `GET` request to the 
                Fruityvice API: */
                string fruityViceURL = String.Format(selectedTemplate1.ApiUrlOne, selectedFruitId);

                // Executing asynchronous `GET` request to Fruityvice API:
                string fruit = await ServiceUtilities.GetFruit(fruityViceURL);
                string fruitFormatted = fruit.ToLower();

                /* Corporate Buzzword Generator API always returns a random string, so not necessary to interpolate an ID value at end 
                of a URL string: */
                string corporateBs = await ServiceUtilities.GetCorporateBuzzwords(selectedTemplate1.ApiUrlTwo);
                string corporateBsFormatted = corporateBs.ToLower();

                /* Populating the `PAPI_Lib` object bound to variable name `papi_lib` with values for all relevant properties and interpolating 
                `fruitFormatted` and `corporateBsFormatted` into `PAPI_Lib` object's `CompletedString` property: */
                _context.PAPI_Libs.Add(ServiceUtilities.PAPI_LibBuilder(papi_lib, selectedTemplate1, fruitFormatted, corporateBsFormatted));
                _context.SaveChanges();
                break;
            case 2:
                PAPI_LibTemplate selectedTemplate2 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                string musicGenre = await ServiceUtilities.GetGenre(selectedTemplate2.ApiUrlOne);
                musicGenre = musicGenre.Trim('"');
                string musicGenreFormatted = ServiceUtilities.UppercaseFirst(musicGenre);

                _context.PAPI_Libs.Add(ServiceUtilities.PAPI_LibBuilder(papi_lib, selectedTemplate2, musicGenreFormatted));
                _context.SaveChanges();
                break;
            case 3:
                PAPI_LibTemplate selectedTemplate3 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                string genrenatorStoryUrl = selectedTemplate3.ApiUrlOne;
                string musicStory = await ServiceUtilities.GetStory(genrenatorStoryUrl);
                string musicStoryFormatted = ServiceUtilities.FormatMusicStory(musicStory);

                _context.PAPI_Libs.Add(ServiceUtilities.PAPI_LibBuilder(papi_lib, selectedTemplate3, musicStoryFormatted));
                _context.SaveChanges();
                break;
            case 4:
                PAPI_LibTemplate selectedTemplate4 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                int selectedBookId = numberGenerator.Next(1, 1001);
                
                string gutendexUrl = String.Format(selectedTemplate4.ApiUrlOne, selectedBookId);

                string book = await ServiceUtilities.GetBook(gutendexUrl);

                _context.PAPI_Libs.Add(ServiceUtilities.PAPI_LibBuilder(papi_lib, selectedTemplate4, book));
                _context.SaveChanges();
                break;
            case 5:
                PAPI_LibTemplate selectedTemplate5 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                /* Similar to the Fruityvice API, The Metropolitan Museum of Art's Collection API does not boast a 
                wonderfully long list of consecutive ID values that can be productively queried via randomly-generated 
                ID values across a single range of integer values. As such, ID values for random pieces of art within 
                the API were selected and stored in the `ArtworkIdList` List found in "ServiceUtilities.cs" as a 
                sample of the kinds of art that could be obtained from this API: */
                int randomIndex = numberGenerator.Next(ServiceUtilities.ArtworkIdList.Count);
                int selectedArtworkId = ServiceUtilities.ArtworkIdList[randomIndex];

                string collectionApiUrl = String.Format(selectedTemplate5.ApiUrlOne, selectedArtworkId);

                string artWork = await ServiceUtilities.GetArtwork(collectionApiUrl);
                
                _context.PAPI_Libs.Add(ServiceUtilities.PAPI_LibBuilder(papi_lib, selectedTemplate5, artWork));
                _context.SaveChanges();
                break;
        }
    }

    public void Delete(int id)
    {
        var pAPI_LibToDelete = _context.PAPI_Libs.Find(id);
        if (pAPI_LibToDelete is not null)
        {
            _context.PAPI_Libs.Remove(pAPI_LibToDelete);
            _context.SaveChanges();
        }        
    }

    public async Task Update(PAPI_Lib papi_lib)
    {
        var pAPI_LibToUpdate = _context.PAPI_Libs.Find(papi_lib.Id);

        if (pAPI_LibToUpdate is null)
        {
            throw new InvalidOperationException("Error: PAPI-Lib with corresponding ID value not found.");
        }

        Random putNumberGenerator = new Random();

        switch(pAPI_LibToUpdate.TemplateId)
        {
            case 1:
                int selectedFruitId = putNumberGenerator.Next(64, 73);

                string fruityViceURL = String.Format(pAPI_LibToUpdate.ApiUrlOne, selectedFruitId);

                string fruit = await ServiceUtilities.GetFruit(fruityViceURL);
                string fruitFormatted = fruit.ToLower();

                string CorporateBuzzwordsURL = pAPI_LibToUpdate.ApiUrlTwo;

                string corporateBs = await ServiceUtilities.GetCorporateBuzzwords(CorporateBuzzwordsURL);
                string corporateBsFormatted = corporateBs.ToLower();

                pAPI_LibToUpdate.CompletedString = $"When life gives you {fruitFormatted}, {corporateBsFormatted}.";

                _context.SaveChanges();
                break;
            case 2:
                string genrenatorGenreUrl = pAPI_LibToUpdate.ApiUrlOne;
                string musicGenre = await ServiceUtilities.GetGenre(genrenatorGenreUrl);
                musicGenre = musicGenre.Trim('"');
                string musicGenreFormatted = ServiceUtilities.UppercaseFirst(musicGenre);

                pAPI_LibToUpdate.CompletedString = $"{musicGenreFormatted} makes the heart grow fonder.";
                
                _context.SaveChanges();
                break;
            case 3:
                string genrenatorStoryUrl = pAPI_LibToUpdate.ApiUrlOne;
                string musicStory = await ServiceUtilities.GetStory(genrenatorStoryUrl);
                string musicStoryFormatted = ServiceUtilities.FormatMusicStory(musicStory);

                pAPI_LibToUpdate.CompletedString = $"As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{musicStoryFormatted}' Jesus said. At once they left their nets and followed him.";
                
                _context.SaveChanges();
                break;
            case 4:
                int selectedBookId = putNumberGenerator.Next(1, 1001);

                string gutendexUrl = String.Format(pAPI_LibToUpdate.ApiUrlOne, selectedBookId);
                string book = await ServiceUtilities.GetBook(gutendexUrl);

                pAPI_LibToUpdate.CompletedString = $"'{book}' is the Guide Which I Will Never Abandon.";

                _context.SaveChanges();
                break;
            case 5:
                int randomIndex = putNumberGenerator.Next(ServiceUtilities.ArtworkIdList.Count);
                int selectedArtworkId = ServiceUtilities.ArtworkIdList[randomIndex];

                string collectionApiUrl = String.Format(pAPI_LibToUpdate.ApiUrlOne, selectedArtworkId);
                string artWork = await ServiceUtilities.GetArtwork(collectionApiUrl);

                pAPI_LibToUpdate.CompletedString = $"I really believe that if you practice enough, you could paint '{artWork}' with a two-inch brush.";

                _context.SaveChanges();
                break;
        }        
    }
}
