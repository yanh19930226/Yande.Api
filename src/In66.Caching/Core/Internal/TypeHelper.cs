namespace In66.Caching.Core.Internal
{
    public static class TypeHelper
    {
        /// <summary>
        /// The subtract full name regex.
        /// </summary>
        private static readonly Regex SubtractFullNameRegex = new Regex(@", Version=\d+.\d+.\d+.\d+, Culture=\w+, PublicKeyToken=\w+", RegexOptions.Compiled);

        /// <summary>
        /// Builds the name of the type.
        /// </summary>
        /// <returns>The type name.</returns>
        /// <param name="type">Type.</param>
        public static string BuildTypeName(Type type)
        {
            return SubtractFullNameRegex.Replace(type.AssemblyQualifiedName, "");
        }
    }
}
