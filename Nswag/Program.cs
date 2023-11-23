using System.Diagnostics;

namespace Nswag
{
    public static class Program
    {
        static async Task Main()
        {
            StartPresentation();

            const string url = "http://localhost:6969/swagger/v1/swagger.json";

            for (var i = 0; i < 30; i++)
            {
                try
                {
                    var spec = await DownloadJsonAsync(url);
                    SaveJsonToFile(spec, "spec.json");

                    const string command = "npx";
                    const string arguments = "nswag openapi2tsclient /input:spec.json /output:../src/Client.ts";
                    RunShellCommand($"{GetProjectDirectory()}/Config", command, arguments);
                    
                    Console.WriteLine("Client generated successfully.");
                    Environment.Exit(0);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{i}] Site bit frosty.");
                    await Task.Delay(1 * 2000);
                }
            }

            throw new Exception("Failed to generate client.");
        }

        private static async Task<string> DownloadJsonAsync(string url)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        private static void SaveJsonToFile(string json, string fileName)
        {
            var filePath = Path.Combine($"{GetProjectDirectory()}/Config", fileName);

            File.WriteAllText(filePath, json);
        }

        private static string GetProjectDirectory()
        {
            var workingDirectory = Environment.CurrentDirectory;
            return Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        }


        static void StartPresentation()
        {
            var presentationPath =
                "/Volumes/Dev2/PrototypeApi/Presentation/Presentation.csproj"; // Replace with the actual path to the other project

            try
            {
                StartOtherProject(presentationPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void StartOtherProject(string projectPath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --project {projectPath}",
                UseShellExecute = true,
                RedirectStandardOutput = false,
                CreateNoWindow = false,
            };

            Process.Start(startInfo);
        }


        private static void RunShellCommand(string workingDirectory, string command, string arguments)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                RedirectStandardInput = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = startInfo;

            process.Start();

            var output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            process.WaitForExit();
        }
    }
}