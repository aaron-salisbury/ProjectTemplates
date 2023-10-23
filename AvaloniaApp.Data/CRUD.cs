using AvaloniaApp.Data.Attributes;
using AvaloniaApp.Data.Domains;
using System.Diagnostics;
using System.Text.Json;

namespace AvaloniaApp.Data
{
    public static class CRUD
    {
        private static InternalStorage _internalStorage = Task.Run(() => InitializeInternalStorageAsync()).Result;

        public static async Task<T?> ReadDataAsync<T>()
        {
            try
            {
                string filePath = Path.Combine(GetIODirectoryPath<T>(), GetJsonFileNameForType(typeof(T)));

                if (File.Exists(filePath))
                {
                    using (FileStream openStream = File.OpenRead(filePath))
                    {
                        return await JsonSerializer.DeserializeAsync<T>(openStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to read from file system: {ex.Message}", "ERROR");
            }

            return default;
        }

        /// <summary>
        /// Look in location of current data type, but for an older format.
        /// </summary>
        /// <typeparam name="T">Old data type.</typeparam>
        /// <typeparam name="U">New data type.</typeparam>
        public static async Task<U?> ReadLegacyDataAsync<T, U>()
        {
            try
            {
                string filePath = Path.Combine(GetIODirectoryPath<T>(), GetJsonFileNameForType(typeof(T)));

                if (File.Exists(filePath))
                {
                    using (FileStream openStream = File.OpenRead(filePath))
                    {
                        return await JsonSerializer.DeserializeAsync<U>(openStream);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to read from file system: {ex.Message}", "ERROR");
            }

            return default;
        }

        public static async Task<bool> UpdateDataAsync<T>(T data)
        {
            if (data != null)
            {
                try
                {
                    FileInfo file = new FileInfo(Path.Combine(GetIODirectoryPath<T>(), GetJsonFileNameForType(typeof(T))));

                    if (file != null && file.Directory != null)
                    {
                        file.Directory.Create();

                        using (FileStream createStream = File.Create(file.FullName))
                        {
                            await JsonSerializer.SerializeAsync<T>(createStream, data);
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to write to file system: {ex.Message}", "ERROR");
                }
            }

            return false;
        }

        /// <typeparam name="T">Application data Type that may now have to be moved (or deleted if one already exists at new location).</typeparam>
        public static async Task<bool> UpdateUserStorageDirectoryAsync<T>(string newUserDirectory)
        {
            bool isSuccessful = false;

            try
            {
                if (!string.IsNullOrEmpty(newUserDirectory))
                {
                    MoveOrDeleteAppData<T>(newUserDirectory);

                    _internalStorage.UserStorageDirectory = newUserDirectory;
                    await UpdateDataAsync<InternalStorage>(_internalStorage);

                    isSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to update internal storage: {ex.Message}", "ERROR");
            }

            return isSuccessful;
        }

        public static void DeleteDomain<T>()
        {
            try
            {
                string filePath = Path.Combine(GetIODirectoryPath<T>(), GetJsonFileNameForType(typeof(T)));

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to delete file: {ex.Message}", "ERROR");
            }
        }

        public static string GetCurrentUserStorageDirectory()
        {
            return _internalStorage.UserStorageDirectory;
        }

        private static async Task<InternalStorage> InitializeInternalStorageAsync()
        {
            InternalStorage? internalStorage = ReadDataAsync<InternalStorage?>()?.Result;

            if (internalStorage != null)
            {
                return internalStorage;
            }
            else
            {
                internalStorage = new InternalStorage() { UserStorageDirectory = GetAppDirectoryPath() };

                await UpdateDataAsync<InternalStorage>(internalStorage);

                return internalStorage;
            }
        }

        private static void MoveOrDeleteAppData<T>(string userDirectory)
        {
            try
            {
                if (!string.IsNullOrEmpty(userDirectory))
                {
                    string appDataFileName = GetJsonFileNameForType(typeof(T));
                    string oldAppDataFilePath = Path.Combine(_internalStorage.UserStorageDirectory, appDataFileName);
                    string newAppDataFilePath = Path.Combine(userDirectory, appDataFileName);

                    if (File.Exists(oldAppDataFilePath))
                    {
                        if (!File.Exists(newAppDataFilePath))
                        {
                            Directory.Move(oldAppDataFilePath, newAppDataFilePath);
                        }
                        else
                        {
                            // If the file already exists at the new location, they chose it for a reason; delete the previous one.
                            CRUD.DeleteDomain<T>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to move/delete application data: {ex.Message}", "ERROR");
            }
        }

        private static string GetIODirectoryPath<T>()
        {
            if (typeof(T) == typeof(InternalStorage))
            {
                return GetAppDirectoryPath();
            }
            else
            {
                return GetCurrentUserStorageDirectory();
            }
        }

        private static string GetAppDirectoryPath()
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string? assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string? appName = assemblyName?.Substring(0, assemblyName.IndexOf('.'));

            return !string.IsNullOrEmpty(appName) ? Path.Combine(appPath, appName) : appPath;
        }

        private static string GetJsonFileNameForType(Type domainType)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(domainType);
            Attribute? baseNameAttribute = attributes.Where(a => a is BaseNameAttribute).FirstOrDefault();
            if (baseNameAttribute != null)
            {
                return $"{((BaseNameAttribute)baseNameAttribute).BaseName}.json";
            }

            string typeName = domainType.ToString();
            int pos = typeName.LastIndexOf('.') + 1;
            string objectName = typeName.Substring(pos, typeName.Length - pos);

            return $"{objectName}.json";
        }
    }
}
