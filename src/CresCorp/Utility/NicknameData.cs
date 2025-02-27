using StorySystem;
using SerializableAttribute = System.SerializableAttribute;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace LimbusLocalizeRUS
{
    [Serializable]
    public class NicknameData : ScenarioAssetData
    {
        [JsonPropertyName("runame")]
        public string? runame;

        [JsonPropertyName("ruNickName")]
        public string? ruNickName;

        public NicknameData() { }

        internal static NicknameData Create(ref Utf8JsonReader reader)
        {
            var result = new NicknameData();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }
                 if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    if (propertyName == "name")
                    {
                        result.name = reader.GetString();
                    }
                    else if (propertyName == "runame")
                    {
                        result.runame = reader.GetString();
                    }
                    else if (propertyName == "ruNickName")
                    {
                        result.ruNickName = reader.GetString();
                    }
                }
            }
            return result;
        }
    }
}