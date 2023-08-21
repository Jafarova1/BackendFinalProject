namespace FinalProject.Helpers
{
    public static class FileExtension
    {
        public static async Task SaveFileAsync(this IFormFile file, string fileName, string basePath, string folder)
        {
            string path = Path.Combine(basePath, folder, fileName);

            using FileStream stream = new(path, FileMode.Create);

            await file.CopyToAsync(stream);

        }
    }
}
