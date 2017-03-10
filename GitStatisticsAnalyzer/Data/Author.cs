namespace GitStatisticsAnalyzer.Data
{
    class Author
    {
        public Author(string authorData)
        {
            var splitedAuthorData = authorData.Split(new string[] { " <" }, 2, System.StringSplitOptions.None);
            Name = splitedAuthorData[0].Replace("Author: ", "");
            Email = splitedAuthorData[1].Replace(">", "");
        }

        public string Email { get; }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
