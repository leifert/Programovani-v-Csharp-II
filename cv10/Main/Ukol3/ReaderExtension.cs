using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukol3
{
    public delegate T Reader<in TEnvironment, out T>(TEnvironment environment);
    public static class ReaderExtensions
    {
        public static Reader<TEnvironment, C> SelectMany<TEnvironment, A, B, C>(
            this Reader<TEnvironment, A> source,
            Func<A, Reader<TEnvironment, B>> selector,
            Func<A, B, C> resultSelector) =>
                environment =>
                {
                    var value = source(environment);
                    var result = selector(value)(environment);
                    return resultSelector(value, result);
                };

        public static Reader<TEnvironment, B> Select<TEnvironment, A, B>(
            this Reader<TEnvironment, A> source,
            Func<A, B> selector) =>
                environment =>
                {
                    var value = source(environment);
                    return selector(value);
                };

        public static void Test()
        {
            Console.WriteLine("Reader test");
            var config = new JsonObject(new []{ 
                new KeyValuePair<string, JsonValue>("input", new JsonPrimitive("test.txt")),
                new KeyValuePair<string, JsonValue>("output", new JsonPrimitive("result.txt")),
                new KeyValuePair<string, JsonValue>("someValue", new JsonPrimitive(1)),});
           
             Reader<JsonObject, string> query =
                from input in (Reader<JsonObject, string>)(reader => reader["input"] )
                from output in (Reader<JsonObject, string>)(reader => reader["output"])
                from value in (Reader<JsonObject, int>)(reader => reader["someValue"])
                select $"Input {input}, Output {output}, value {value}";

            var result = query(config);

            Console.WriteLine(result);
        }
    }
}
