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

                string CorporateBuzzwordsURL = selectedTemplate1.ApiUrlTwo;

                string corporateBs = await ServiceUtilities.GetCorporateBuzzwords(CorporateBuzzwordsURL);
                string corporateBsFormatted = corporateBs.ToLower();

                papi_lib.PAPI_LibTemplate = selectedTemplate1.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate1.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate1.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate1.Id;
                papi_lib.CompletedString = $"When life gives you {fruitFormatted}, {corporateBsFormatted}.";
                papi_lib.ApiUrlOne = selectedTemplate1.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate1.ApiNameOne;
                papi_lib.ApiUrlTwo = selectedTemplate1.ApiUrlTwo;
                papi_lib.ApiNameTwo = selectedTemplate1.ApiNameTwo;

                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 2:
                PAPI_LibTemplate selectedTemplate2 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                string genrenatorGenreUrl = selectedTemplate2.ApiUrlOne;
                string musicGenre = await ServiceUtilities.GetGenre(genrenatorGenreUrl);
                musicGenre = musicGenre.Trim('"');
                string musicGenreFormatted = ServiceUtilities.UppercaseFirst(musicGenre);

                papi_lib.PAPI_LibTemplate = selectedTemplate2.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate2.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate2.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate2.Id;
                papi_lib.CompletedString = $"{musicGenreFormatted} makes the heart grow fonder.";
                papi_lib.ApiUrlOne = selectedTemplate2.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate2.ApiNameOne;

                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 3:
                PAPI_LibTemplate selectedTemplate3 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                string genrenatorStoryUrl = selectedTemplate3.ApiUrlOne;
                string musicStory = await ServiceUtilities.GetStory(genrenatorStoryUrl);
                /* (3/25/24, 1) Candidate for FURTHER refactoring immediately below this (?): */
                /* musicStory = musicStory.Trim('"');
                string musicStoryFormatted = musicStory.Remove(musicStory.Length - 1);
                musicStoryFormatted += ","; */
                string musicStoryFormatted = ServiceUtilities.FormatMusicStory(musicStory);

                papi_lib.PAPI_LibTemplate = selectedTemplate3.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate3.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate3.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate3.Id;
                papi_lib.CompletedString = $"As Jesus was walking beside the Sea of Galilee, he saw two brothers, Simon called Peter and his brother Andrew. They were casting a net into the lake, for they were fishermen. '{musicStoryFormatted}' Jesus said. At once they left their nets and followed him.";
                papi_lib.ApiUrlOne = selectedTemplate3.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate3.ApiNameOne;

                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 4:
                PAPI_LibTemplate selectedTemplate4 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                int selectedBookId = numberGenerator.Next(1, 1001);
                
                string gutendexUrl = String.Format(selectedTemplate4.ApiUrlOne, selectedBookId);

                string book = await ServiceUtilities.GetBook(gutendexUrl);

                papi_lib.PAPI_LibTemplate = selectedTemplate4.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate4.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate4.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate4.Id;
                papi_lib.CompletedString = $"'{book}' is the Guide Which I Will Never Abandon.";
                papi_lib.ApiUrlOne = selectedTemplate4.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate4.ApiNameOne;

                _context.PAPI_Libs.Add(papi_lib);
                _context.SaveChanges();
                break;
            case 5:
                PAPI_LibTemplate selectedTemplate5 = _context.PAPI_LibTemplates.Find(selectedTemplateId);

                List<int> idList = new List<int>
                {
                    16460,
                    72520,
                    207528,
                    261876,
                    259686,
                    269278,
                    270797,
                    282161,
                    344619,
                    347567,
                    907116,
                    905064,
                    889576,
                    888476,
                    853075,
                    814822,
                    815150,
                    787967,
                    782239,
                    756448,
                    751191,
                    716307,
                    698842,
                    687539,
                    569710,
                    495324
                };

                int randomIndex = numberGenerator.Next(idList.Count);
                int selectedArtworkId = idList[randomIndex];

                string collectionApiUrl = String.Format(selectedTemplate5.ApiUrlOne, selectedArtworkId);

                string artWork = await ServiceUtilities.GetArtwork(collectionApiUrl);
                
                papi_lib.PAPI_LibTemplate = selectedTemplate5.TemplateString;
                papi_lib.OriginalQuote = selectedTemplate5.OriginalQuote;
                papi_lib.OriginalQuoteAuthorOrSource = selectedTemplate5.OriginalQuoteAuthorOrSource;
                papi_lib.TemplateId = selectedTemplate5.Id;
                papi_lib.CompletedString = $"I really believe that if you practice enough, you could paint '{artWork}' with a two-inch brush.";
                papi_lib.ApiUrlOne = selectedTemplate5.ApiUrlOne;
                papi_lib.ApiNameOne = selectedTemplate5.ApiNameOne;

                _context.PAPI_Libs.Add(papi_lib);
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
                /* (3/25/24, 1) Candidate for FURTHER refactoring immediately below this (?): */
                /* musicStory = musicStory.Trim('"');
                string musicStoryFormatted = musicStory.Remove(musicStory.Length - 1);
                musicStoryFormatted += ","; */
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
                List<int> idList = new List<int>
                {
                    16460,
                    72520,
                    207528,
                    261876,
                    259686,
                    269278,
                    270797,
                    282161,
                    344619,
                    347567,
                    907116,
                    905064,
                    889576,
                    888476,
                    853075,
                    814822,
                    815150,
                    787967,
                    782239,
                    756448,
                    751191,
                    716307,
                    698842,
                    687539,
                    569710,
                    495324
                };

                int randomIndex = putNumberGenerator.Next(idList.Count);
                int selectedArtworkId = idList[randomIndex];

                string collectionApiUrl = String.Format(pAPI_LibToUpdate.ApiUrlOne, selectedArtworkId);
                string artWork = await ServiceUtilities.GetArtwork(collectionApiUrl);

                pAPI_LibToUpdate.CompletedString = $"I really believe that if you practice enough, you could paint '{artWork}' with a two-inch brush.";

                _context.SaveChanges();
                break;
        }        
    }
}