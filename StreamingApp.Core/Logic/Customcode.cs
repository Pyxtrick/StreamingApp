namespace StreamingApp.Core.Logic;

// Ascii to text to Ascii
public class Customcode
{
    private readonly Dictionary<char, string> _specialCode = new Dictionary<char, string>()
    {
        {'Ä', "10000100"}, {'ä', "10100100"},
        {'Ö', "10010110"}, {'ö', "10110110"},
        {'Ü', "10011100"}, {'ü', "10111100"},
    };

    private readonly Dictionary<char, string> _customCode = new Dictionary<char, string>()
    {
        {'A', "01000001"}, {'a', "01100001"},
        {'B', "01000010"}, {'b', "01100010"},
        {'C', "01000011"}, {'c', "01100011"},
        {'D', "01000100"}, {'d', "01100100"},
        {'E', "01000101"}, {'e', "01100101"},
        {'F', "01000110"}, {'f', "01100110"},
        {'G', "01000111"}, {'g', "01100111"},
        {'H', "01001000"}, {'h', "01101000"},
        {'I', "01001001"}, {'i', "01101001"},
        {'J', "01001010"}, {'j', "01101010"},
        {'K', "01001011"}, {'k', "01101011"},
        {'L', "01001100"}, {'l', "01101100"},
        {'M', "01001101"}, {'m', "01101101"},
        {'N', "01001110"}, {'n', "01101110"},
        {'O', "01001111"}, {'o', "01101111"},
        {'P', "01010000"}, {'p', "01110000"},
        {'Q', "01010001"}, {'q', "01110001"},
        {'R', "01010010"}, {'r', "01110010"},
        {'S', "01010011"}, {'s', "01110011"},
        {'T', "01010100"}, {'t', "01110100"},
        {'U', "01010101"}, {'u', "01110101"},
        {'V', "01010110"}, {'v', "01110110"},
        {'W', "01010111"}, {'w', "01110111"},
        {'X', "01011000"}, {'x', "01111000"},
        {'Y', "01011001"}, {'y', "01111001"},
        {'Z', "01011010"}, {'z', "01111010"},
        {'0', "00110000"},
        {'1', "00110001"},
        {'2', "00110010"},
        {'3', "00110011"},
        {'4', "00110100"},
        {'5', "00110101"},
        {'6', "00110110"},
        {'7', "00110111"},
        {'8', "00111000"},
        {'9', "00111001"},
        {'.', "00101110"},
        {',', "00101100"},
        {'?', "00111111"},
        {'\'', "01011100"},
        {'!', "00100001"},
        {'/', "00101111"},
        {'(', "00101000"},
        {')', "00101001"},
        {'&', "00100110"},
        {':', "00111010"},
        {';', "00111011"},
        {'=', "00111101"},
        {'+', "00101011"},
        {'-', "00101101"},
        {'_', "01011111"},
        {'\"', "01011100"},
        {'$', "00100100"},
        {'@', "01000000"},
        {' ', "00100000"},
    };

    public string Encode(string message)
    {
        string encodedMessage = "";
        bool specialCharacter = false;
        foreach (char character in message)
        {
            if (specialCharacter && _specialCode.ContainsKey(character))
            {
                specialCharacter = false;
                encodedMessage += _specialCode[character] + " ";
            }
            else if (!specialCharacter && _customCode.ContainsKey(character))
            {
                if (character.Equals("11000011"))
                {
                    specialCharacter = true;
                }
                else
                {
                    encodedMessage += _customCode[character] + " ";
                }
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
                foreach (KeyValuePair<char, string> kvp in _customCode)
                {
                    if (letter == kvp.Value)
                    {
                        decodedMessage += kvp.Key;
                        break;
                    }
                }
                foreach (KeyValuePair<char, string> kvp in _specialCode)
                {
                    if (letter == kvp.Value)
                    {
                        decodedMessage += "11000011 " + kvp.Key;
                        break;
                    }
                }
            }
            decodedMessage += " ";
        }
        return decodedMessage.Trim();
    }
}
