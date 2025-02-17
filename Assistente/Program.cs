using Azure.AI.OpenAI; 
using Azure;
using OpenAI.Chat;

string openAIEndpoint = "";
string openAIAPIKey = "";
string openAIDeploymentName = "";

var endpoint = new Uri(openAIEndpoint);
var credentials = new AzureKeyCredential(openAIAPIKey);

var openAIClient = new AzureOpenAIClient(endpoint, credentials);

var systemPrompt = """
You are a game enthusiast who helps people discover best games. You are nerd and friendly. 
You ask people what type of games they like to take and then suggest some.
""";
#pragma warning restore CS0219 // Variable is assigned but its value is never used

List<ChatMessage> chatHistory = new();
SystemChatMessage systemMessage = ChatMessage.CreateSystemMessage(systemPrompt);

chatHistory.Add(systemMessage);

string userGreeting =
"Olá, me recomende um jogo para PS4";

UserChatMessage userGreetingMessage = ChatMessage.CreateUserMessage(userGreeting);
chatHistory.Add(userGreetingMessage);

Console.WriteLine($"User >>> {userGreeting}");

var chatClient = openAIClient.GetChatClient(openAIDeploymentName);
var response = await chatClient.CompleteChatAsync(chatHistory);
Console.WriteLine($"AI >>> {response.Value.Content.Last().Text}");