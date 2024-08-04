namespace StreamingApp.Core.Logic;

public class Morsecode
{
    private readonly Dictionary<char, string> _morseCode = new Dictionary<char, string>()
    {
        {'A', ".-"},
        {'B', "-..."},
        {'C', "-.-."},
        {'D', "-.."},
        {'E', "."},
        {'F', "..-."},
        {'G', "--."},
        {'H', "...."},
        {'I', ".."},
        {'J', ".---"},
        {'K', "-.-"},
        {'L', ".-.."},
        {'M', "--"},
        {'N', "-."},
        {'O', "---"},
        {'P', ".--."},
        {'Q', "--.-"},
        {'R', ".-."},
        {'S', "..."},
        {'T', "-"},
        {'U', "..-"},
        {'V', "...-"},
        {'W', ".--"},
        {'X', "-..-"},
        {'Y', "-.--"},
        {'Z', "--.."},
        {'0', "-----"},
        {'1', ".----"},
        {'2', "..---"},
        {'3', "...--"},
        {'4', "....-"},
        {'5', "....."},
        {'6', "-...."},
        {'7', "--..."},
        {'8', "---.."},
        {'9', "----."},
        {'.', ".-.-.-"},
        {',', "--..--"},
        {'?', "..--.."},
        {'\'', ".----."},
        {'!', "-.-.--"},
        {'/', "-..-."},
        {'(', "-.--."},
        {')', "-.--.-"},
        {'&', ".-..."},
        {':', "---..."},
        {';', "-.-.-."},
        {'=', "-...-"},
        {'+', ".-.-."},
        {'-', "-....-"},
        {'_', "..--.-"},
        {'\"', ".-..-."},
        {'$', "...-..-"},
        {'@', ".--.-."},
        {' ', "/"}
    };

    public string Encode(string message)
    {
        message = message.ToUpper();
        string encodedMessage = "";
        foreach (char character in message)
        {
            if (_morseCode.ContainsKey(character))
            {
                encodedMessage += _morseCode[character] + " ";
            }
            else
            {
                encodedMessage += character + " ";
            }
        }
        return encodedMessage.Trim();
    }

    public string Decode(string message)
    {
        string[] words = message.Split('/');
        string decodedMessage = "";
        foreach (string word in words)
        {
            string[] letters = word.Split(' ');
            foreach (string letter in letters)
            {
                foreach (KeyValuePair<char, string> kvp in _morseCode)
                {
                    if (letter == kvp.Value)
                    {
                        decodedMessage += kvp.Key;
                        break;
                    }
                }
            }
            decodedMessage += " ";
        }
        return decodedMessage.Trim();
    }
}