using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Globalization;

namespace LimbusLocalizeRUS
{
    internal class CresConverter
    {
    }


    ///     Конвертирует данные в массив объектов
    internal class NicknameDataEnumerableConverter : JsonConverter<IEnumerable<NicknameData>>
    {

        public override IEnumerable<NicknameData> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = new List<NicknameData>();
            while (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Read();
            }

            while (reader.Read())
            {
                var newCandleStick = NicknameData.Create(ref reader);
                result.Add(newCandleStick);
            }

            return result;
        }


        public override void Write(Utf8JsonWriter writer, IEnumerable<NicknameData> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }


    ///     Нормально конвертирует полученные данные
    internal class NicknameDataConverter : JsonConverter<NicknameData>
    {
        public override NicknameData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return NicknameData.Create(ref reader);
        }


        public override void Write(Utf8JsonWriter writer, NicknameData value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
    public static class Utf8JsonReaderExtension
    {

        ///     Пропускает все, пока не дойдет до требуемой секции
        public static void SkipToSection(this ref Utf8JsonReader reader, string sectionName)
        {
            var lastPropertyName = "";
            var isSkipNeeded = true;
            while (reader.Read() && isSkipNeeded)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    lastPropertyName = reader.GetString();
                    reader.Read();

                    isSkipNeeded = lastPropertyName != sectionName;
                }
            }
        }


        ///     Считывает значение и сдвигает позицию reader'a
        public static double ReadDoubleAndNext(this ref Utf8JsonReader reader)
        {
            var res = double.Parse(reader.GetString(), CultureInfo.InvariantCulture);
            reader.Read();

            return res;
        }


        ///     Считывает значение и сдвигает позицию reader'a
        public static long ReadLongAndNext(this ref Utf8JsonReader reader)
        {
            var res = reader.GetInt64();
            reader.Read();

            return res;
        }


        ///     Считывает значение и сдвигает позицию reader'a
        public static string ReadStringAndNext(this ref Utf8JsonReader reader)
        {
            var res = reader.GetString();
            reader.Read();

            return res;
        }

        ///     Считывает значение и сдвигает позицию reader'a
        public static int ReadIntAndNext(this ref Utf8JsonReader reader)
        {
            var res = reader.GetInt32();
            reader.Read();

            return res;
        }
    }
}
