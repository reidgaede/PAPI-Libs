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
        int selectedTemplateId = numberGenerator.Next(1, 6);
        
        switch(selectedTemplateId)
        {
            case 1:
                PAPI_LibTemplate selectedTemplate1 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                int selectedFruitId = numberGenerator.Next(64, 73);

                string fruityViceURL = String.Format(selectedTemplate1.ApiUrlOne, selectedFruitId);

                string fruit = await ServiceUtilities.GetFruit(fruityViceURL);
                string fruitFormatted = fruit.ToLower();

                string corporateBs = await ServiceUtilities.GetCorporateBuzzwords(selectedTemplate1.ApiUrlTwo);
                string corporateBsFormatted = corporateBs.ToLower();

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