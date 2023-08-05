using System;

namespace Core.Utils
{
    public static class ArrayUtils
    {
        public static TOutput[] ConvertAll<TInput, TOutput>(
            TInput[] array,
            Converter<TInput, TOutput> converter)
        {
            if (array == null)
                return Array.Empty<TOutput>();
            
            return Array.ConvertAll(array, converter);
        }
    }
}