using System.Text;
using System.Text.Json.Nodes;
using ChatBot.Handling.Actions;

public class ActionCreator
{
    public static IDictionary<string, BotAction> GetTextBotActionsDictionary()
    {
        var textJsonNodePairs = Extensions.GetCommandActionJsonNodePairs();
        var textBotActionDictionary = new Dictionary<string, BotAction>();
        foreach (var pair in textJsonNodePairs)
        {
            BotAction? botAction = CreateBotAction(pair.Value);
            if (botAction != null)
                textBotActionDictionary.Add(pair.Key, botAction);

        }
        return textBotActionDictionary;
    }

    private static BotAction? CreateBotAction(JsonNode actionNode)
    {
        string actionName = actionNode["Name"]?.AsValue().ToString()!;
        if (actionName == null)
        {
            Console.WriteLine("Action property in json doesn't contain 'Name' property");
        }

        BotAction? botAction;
        switch (actionName)
        {
            case "Text":
                botAction = CreateSendTextBotAction(actionNode);
                break;
            case "Photo":
                botAction = CreateSendPhotoAction(actionNode);
                break;
            case "Audio":
                botAction = CreateAudioAction(actionNode);
                break;
            case "Voice":
                botAction = CreateVoiceAction(actionNode);
                break;
            case "Video":
                botAction = CreateVideoAction(actionNode);
                break;
            case "VideoNote":
                botAction = CreateVideoNoteAction(actionNode);
                break;
            case "Document":
                botAction = CreateDocumentAction(actionNode);
                break;
            case "Album":
                botAction = CreateAlbumAction(actionNode);
                break;
            default:
                botAction = null;
                break;
        }
        if (botAction == null)
        {
            Console.WriteLine("Action property incorrect");
            return null;
        }
        return botAction;
    }

    private static SendAlbumAction? CreateAlbumAction(JsonNode jsonNode)
    {
        List<string> possibleAlbumItemTypes = new() { "Photo", "Video", "Audio", "Document" };
        List<KeyValuePair<string, string>> albumItems = new();
        JsonNode? filesNode = jsonNode["Files"];
        if (filesNode == null)
        {
            Console.WriteLine("Album Action property in json doesn'y containt 'Files' property");
            return null;
        }
        if (filesNode.AsArray().Count == 0)
        {
            Console.WriteLine("Album action files property must contain members with 'Type' and 'FileId' properties");
            return null;
        }
        foreach (var itemNode in filesNode.AsArray())
        {
            if (itemNode == null)
                return null;

            string? itemType = itemNode["Type"]?.AsValue().ToString();
            string? itemFileId = itemNode["FileId"]?.AsValue().ToString();
            if (itemType == null || itemFileId == null)
            {
                System.Console.WriteLine("Album action files property must contain members with 'Type' and 'FileId' properties");
                return null;
            }
            if (!possibleAlbumItemTypes.Contains(itemType))
            {
                StringBuilder possibleTypes = new StringBuilder();
                foreach (var type in possibleAlbumItemTypes)
                {
                    possibleTypes.Append('\'' + type + "' ");
                }
                System.Console.WriteLine($"Album item type must be one of the followings: {possibleTypes}");
                return null;
            }
            // If possible types are already changed
            if (possibleAlbumItemTypes.Count > 2)
                switch (itemType)
                {
                    case "Photo":
                    case "Video":
                        possibleAlbumItemTypes.RemoveAll(item => item == "Audio" || item == "Document");
                        break;
                    case "Document":
                        possibleAlbumItemTypes = new List<string>() { "Document" };
                        break;
                    case "Audio":
                        possibleAlbumItemTypes = new List<string>() { "Audio" };
                        break;
                }
            albumItems.Add(KeyValuePair.Create(itemType, itemFileId));
        }

        return new SendAlbumAction(albumItems);
    }

    private static SendDocumentAction? CreateDocumentAction(JsonNode jsonNode)
    {
        string? fileId = jsonNode["FileId"]?.AsValue().ToString();
        if (fileId == null)
        {
            Console.WriteLine("Document Action property in json doesn't contain 'File Id' property");
            return null;
        }
        string? caption = jsonNode["Caption"]?.AsValue().ToString();
        return new SendDocumentAction(fileId, caption);
    }

    private static SendVideoAction? CreateVideoAction(JsonNode jsonNode)
    {
        string? fileId = jsonNode["FileId"]?.AsValue().ToString();
        if (fileId == null)
        {
            Console.WriteLine("Video Action property in json doesn't contain 'File Id' property");
            return null;
        }
        string? caption = jsonNode["Caption"]?.AsValue().ToString();
        return new SendVideoAction(fileId, caption);
    }
    private static SendVideoNoteAction? CreateVideoNoteAction(JsonNode jsonNode)
    {
        string? fileId = jsonNode["FileId"]?.AsValue().ToString();
        if (fileId == null)
        {
            Console.WriteLine("VideoNote Action property in json doesn't contain 'File Id' property");
            return null;
        }
        return new SendVideoNoteAction(fileId);
    }
    private static SendVoiceAction? CreateVoiceAction(JsonNode jsonNode)
    {
        string? fileId = jsonNode["FileId"]?.AsValue().ToString();
        if (fileId == null)
        {
            Console.WriteLine("Voice Action property in json doesn't contain 'File Id' property");
            return null;
        }
        string? caption = jsonNode["Caption"]?.AsValue().ToString();
        return new SendVoiceAction(fileId, caption);
    }

    private static SendAudioAction? CreateAudioAction(JsonNode jsonNode)
    {
        string? fileId = jsonNode["FileId"]?.AsValue().ToString();
        if (fileId == null)
        {
            Console.WriteLine("Audio Action property in json doesn't contain 'File Id' property");
            return null;
        }
        string? caption = jsonNode["Caption"]?.AsValue().ToString();
        return new SendAudioAction(fileId, caption);
    }

    private static SendPhotoAction? CreateSendPhotoAction(JsonNode jsonNode)
    {
        string? fileId = jsonNode["FileId"]?.AsValue().ToString();
        if (fileId == null)
        {
            Console.WriteLine("Photo Action property in json doesn't contain 'File Id' property");
            return null;
        }
        string? caption = jsonNode["Caption"]?.AsValue().ToString();
        return new SendPhotoAction(fileId, caption);
    }

    private static SendTextBotAction? CreateSendTextBotAction(JsonNode jsonNode)
    {
        string? textToWrite = jsonNode["Value"]?.AsValue().ToString();
        if (textToWrite == null)
        {
            Console.WriteLine("Text Action property in json doesn't contain 'Value' property");
            return null;
        }

        bool isReply = jsonNode["Reply"] == null ? false : (bool)jsonNode["Reply"]!.AsValue();
        return new SendTextBotAction(textToWrite, isReply);
    }
}