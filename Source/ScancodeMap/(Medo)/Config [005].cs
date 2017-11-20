//Copyright 2017 by Josip Medved <jmedved@jmedved.com> (www.medo64.com) MIT License

//2017-11-05: Suppress exception on UnauthorizedAccessException.
//2017-10-09: Support for /opt installation on Linux.
//2017-04-29: Added IsAssumedInstalled property.
//            Added Reset and DeleteAll methods.
//2017-04-26: Renamed from Properties.
//            Added \0 escape sequence.
//            Fixed alignment issues.
//2017-04-17: First version.


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Medo.Configuration {
    /// <summary>
    /// Provides cached access to reading and writing settings.
    /// This class is thread-safe.
    /// </summary>
    /// <remarks>
    /// File name is the same as name of the &lt;executable&gt;.cfg under windows or .&lt;executable&gt; under Linux.
    /// File format is as follows:
    /// * hash characters (#) denotes comment.
    /// * key and value are colon (:) separated although equals (=) is also supported.
    /// * backslash (\) is used for escaping.
    /// </remarks>
    public static class Config {

        /// <summary>
        /// Returns the value for the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">The value to return if the key does not exist.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static string Read(string key, string defaultValue) {
            key = key?.Trim() ?? throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            if (key.Length == 0) { throw new ArgumentOutOfRangeException(nameof(key), "Key cannot be empty."); }

            lock (SyncReadWrite) {
                if (!IsLoaded) { Load(); }
                if (OverridePropertiesFile != null) {
                    return OverridePropertiesFile.ReadOne(key) ?? DefaultPropertiesFile.ReadOne(key) ?? defaultValue;
                } else {
                    return DefaultPropertiesFile.ReadOne(key) ?? defaultValue;
                }
            }
        }

        /// <summary>
        /// Returns all the values for the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static IEnumerable<string> Read(string key) {
            key = key?.Trim() ?? throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            if (key.Length == 0) { throw new ArgumentOutOfRangeException(nameof(key), "Key cannot be empty."); }

            lock (SyncReadWrite) {
                if (!IsLoaded) { Load(); }
                if (OverridePropertiesFile != null) {
                    var list = new List<string>(OverridePropertiesFile.ReadMany(key));
                    if (list.Count > 0) { return list; }
                }
                return DefaultPropertiesFile.ReadMany(key);
            }
        }


        /// <summary>
        /// Returns the value for the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">The value to return if the key does not exist.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static bool Read(string key, bool defaultValue) {
            if (Read(key, null) is string value) {
                if (bool.TryParse(value, out var result)) {
                    return result;
                } else if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var resultInt)) {
                    return (resultInt != 0);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Returns the value for the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">The value to return if the key does not exist.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static int Read(string key, int defaultValue) {
            if (Read(key, null) is string value) {
                if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Returns the value for the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">The value to return if the key does not exist.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static long Read(string key, long defaultValue) {
            if (Read(key, null) is string value) {
                if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result)) {
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Returns the value for the specified key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="defaultValue">The value to return if the key does not exist.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static double Read(string key, double defaultValue) {
            if (Read(key, null) is string value) {
                if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var result)) {
                    return result;
                }
            }
            return defaultValue;
        }


        /// <summary>
        /// Writes the value for the specified key.
        /// If the specified key does not exist, it is created.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">The value to write.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null. -or- Value cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Write(string key, string value) {
            key = key?.Trim() ?? throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            if (key.Length == 0) { throw new ArgumentOutOfRangeException(nameof(key), "Key cannot be empty."); }
            if (value == null) { throw new ArgumentNullException(nameof(key), "Value cannot be null."); }

            lock (SyncReadWrite) {
                if (!IsLoaded) { Load(); }
                DefaultPropertiesFile.WriteOne(key, value);
                if (ImmediateSave) { Save(); }
            }
        }

        /// <summary>
        /// Writes the values for the specified key.
        /// If the specified key does not exist, it is created.
        /// If value is null or empty, key is deleted.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">The value to write.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Write(string key, IEnumerable<string> value) {
            key = key?.Trim() ?? throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            if (key.Length == 0) { throw new ArgumentOutOfRangeException(nameof(key), "Key cannot be empty."); }

            lock (SyncReadWrite) {
                if (!IsLoaded) { Load(); }
                DefaultPropertiesFile.WriteMany(key, value);
                if (ImmediateSave) { Save(); }
            }
        }

        /// <summary>
        /// Writes the value for the specified key.
        /// If the specified key does not exist, it is created.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">The value to write.</param>4
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Write(string key, bool value) {
            Write(key, value ? "true" : "false"); //not using ToString() because it would capitalize first letter
        }

        /// <summary>
        /// Writes the value for the specified key.
        /// If the specified key does not exist, it is created.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">The value to write.</param>4
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Write(string key, int value) {
            Write(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes the value for the specified key.
        /// If the specified key does not exist, it is created.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">The value to write.</param>4
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Write(string key, long value) {
            Write(key, value.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Writes the value for the specified key.
        /// If the specified key does not exist, it is created.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">The value to write.</param>4
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Write(string key, double value) {
            Write(key, value.ToString("r", CultureInfo.InvariantCulture));
        }


        /// <summary>
        /// Deletes key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <exception cref="ArgumentNullException">Key cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Key cannot be empty.</exception>
        public static void Delete(string key) {
            key = key?.Trim() ?? throw new ArgumentNullException(nameof(key), "Key cannot be null.");
            if (key.Length == 0) { throw new ArgumentOutOfRangeException(nameof(key), "Key cannot be empty."); }

            lock (SyncReadWrite) {
                if (!IsLoaded) { Load(); }
                DefaultPropertiesFile.Delete(key);
                if (ImmediateSave) { Save(); }
            }
        }


        #region Loading and saving

        private static bool IsInitialized { get; set; }
        private static bool IsLoaded { get; set; }
        private static PropertiesFile DefaultPropertiesFile;
        private static PropertiesFile OverridePropertiesFile;
        private static readonly object SyncReadWrite = new object();


        private static bool IsAssumedInstalledBacking;
        /// <summary>
        /// Gets if application is assumed to be installed.
        /// Application is considered installed if it is located in Program Files directory (or opt) or if file is already present in Application Data folder.
        /// </summary>
        public static bool IsAssumedInstalled {
            get {
                lock (SyncReadWrite) {
                    if (!IsInitialized) { Initialize(); }
                    return IsAssumedInstalledBacking;
                }
            }
        }

        private static string FileNameBacking;
        /// <summary>
        /// Gets/sets the name of the file used for settings.
        /// If executable is located under Program Files, properties file will be under Application Data.
        /// If executable is located in some other directory, a local file will be used.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Value is not a valid path.</exception>
        public static string FileName {
            get {
                lock (SyncReadWrite) {
                    if (!IsInitialized) { Initialize(); }
                    return FileNameBacking;
                }
            }
            set {
                lock (SyncReadWrite) {
                    if (!IsInitialized) { Initialize(); }
                    if (value == null) {
                        throw new ArgumentNullException(nameof(value), "Value cannot be null.");
                    } else if (value.IndexOfAny(Path.GetInvalidPathChars()) >= 0) {
                        throw new ArgumentOutOfRangeException(nameof(value), "Value is not a valid path.");
                    } else {
                        FileNameBacking = value;
                        IsLoaded = false; //force loading
                    }
                }
            }
        }

        private static string OverrideFileNameBacking;
        /// <summary>
        /// Gets/sets the name of the file used for settings override.
        /// This file is not written to.
        /// If executable is located under Program Files, override properties file will be in executable's directory.
        /// If executable is located in some other directory, no override file will be used.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Value is not a valid path.</exception>
        public static string OverrideFileName {
            get {
                lock (SyncReadWrite) {
                    if (!IsInitialized) { Initialize(); }
                    return OverrideFileNameBacking;
                }
            }
            set {
                lock (SyncReadWrite) {
                    if (!IsInitialized) { Initialize(); }
                    if ((value != null) && (value.IndexOfAny(Path.GetInvalidPathChars()) >= 0)) {
                        throw new ArgumentOutOfRangeException(nameof(value), "Value is not a valid path.");
                    } else {
                        OverrideFileNameBacking = value;
                        IsLoaded = false;
                    }
                }
            }
        }


        /// <summary>
        /// Loads all settings from a file.
        /// Returns true if file was found.
        /// </summary>
        /// <param name="fileName">File name to use.</param>
        public static bool Load(string fileName) {
            lock (SyncReadWrite) {
                FileName = fileName;
                OverrideFileName = null;
                return Load();
            }
        }

        /// <summary>
        /// Loads all settings from a file.
        /// Returns true if file was found.
        /// </summary>
        public static bool Load() {
            var sw = Stopwatch.StartNew();
            try {
                lock (SyncReadWrite) {
                    OverridePropertiesFile = (OverrideFileName != null) ? new PropertiesFile(OverrideFileName, isOverride: true) : null;
                    DefaultPropertiesFile = new PropertiesFile(FileName);
                    IsLoaded = true;
                    return DefaultPropertiesFile.FileExists;
                }
            } finally {
                Debug.WriteLine("[Settings] Load completed in " + sw.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) + " milliseconds.");
            }
        }

        /// <summary>
        /// Saves all settings to a file.
        /// Returns true if action is successful.
        /// </summary>
        public static bool Save() {
            var sw = Stopwatch.StartNew();
            try {
                lock (SyncReadWrite) {
                    if (!IsLoaded) { Load(); }
                    return DefaultPropertiesFile.Save();
                }
            } finally {
                Debug.WriteLine("[Settings] Save completed in " + sw.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) + " milliseconds.");
            }
        }


        /// <summary>
        /// Deletes all settings.
        /// </summary>
        public static void DeleteAll() {
            lock (SyncReadWrite) {
                if (!IsLoaded) { Load(); }
                DefaultPropertiesFile.DeleteAll();
                if (ImmediateSave) { Save(); }
            }
            Debug.WriteLine("[Settings] Settings deleted.");
        }


        /// <summary>
        /// Resets configuration. This includes file names and installation status.
        /// </summary>
        public static void Reset() {
            lock (SyncReadWrite) {
                IsLoaded = false;
                IsInitialized = false;
            }
        }

        /// <summary>
        /// Gets/sets if setting is saved immediatelly.
        /// </summary>
        public static bool ImmediateSave { get; set; } = true;


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Lower case form of application name is only used to generate properties file name which is not compared against other text.")]
        private static void Initialize() {
            var assembly = Assembly.GetEntryAssembly();

            string companyValue = null;
            string productValue = null;
            string titleValue = null;

#if NETSTANDARD1_6
            var attributes = assembly.GetCustomAttributes();
#else
            var attributes = assembly.GetCustomAttributes(true);
#endif
            foreach (var attribute in attributes) {
                if (attribute is AssemblyCompanyAttribute companyAttribute) { companyValue = companyAttribute.Company.Trim(); }
                if (attribute is AssemblyProductAttribute productAttribute) { productValue = productAttribute.Product.Trim(); }
                if (attribute is AssemblyTitleAttribute titleAttribute) { titleValue = titleAttribute.Title.Trim(); }
            }

            var company = companyValue ?? "";
            var application = productValue ?? titleValue ?? assembly.GetName().Name;
            var executablePath = assembly.Location;

            var baseFileName = IsOSWindows
                ? application + ".cfg"
                : "." + application.ToLowerInvariant();

            var userFileLocation = IsOSWindows
                ? Path.Combine(Environment.GetEnvironmentVariable("AppData"), company, application, baseFileName)
                : Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "~", baseFileName);

            var priorityFileLocation = Path.Combine(Path.GetDirectoryName(executablePath), baseFileName);

            bool isInProgramFiles;
            if (IsOSWindows) {
                var isPF = executablePath.StartsWith(Environment.GetEnvironmentVariable("ProgramFiles") + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
                var isPF32 = executablePath.StartsWith(Environment.GetEnvironmentVariable("ProgramFiles(x86)") + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
                isInProgramFiles = isPF || isPF32;
            } else {
                var isOpt = executablePath.StartsWith(Path.DirectorySeparatorChar + "opt" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
                var isBin = executablePath.StartsWith(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
                var isUsrBin = executablePath.StartsWith(Path.DirectorySeparatorChar + "usr" + Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase);
                isInProgramFiles = isOpt || isBin || isUsrBin;
                if (isOpt) { //change priority file location to /etc/opt/<app>/<app>.cfg
                    priorityFileLocation = Path.DirectorySeparatorChar + "etc" + Path.Combine(Path.GetDirectoryName(executablePath), application.ToLowerInvariant() + ".conf");
                }
            }

            IsAssumedInstalledBacking = File.Exists(userFileLocation) || isInProgramFiles;
            FileNameBacking = IsAssumedInstalledBacking ? userFileLocation : priorityFileLocation;
            OverrideFileNameBacking = IsAssumedInstalledBacking ? priorityFileLocation : null; //no priority file - one in current directory is the default one

            IsInitialized = true;
        }

#if NETSTANDARD2_0 || NETSTANDARD1_6
        private static bool IsOSWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#else
        private static bool IsOSWindows => (Type.GetType("Mono.Runtime") == null);
#endif

        #region PropertiesFile

        private class PropertiesFile {

            private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            private static readonly StringComparer KeyComparer = StringComparer.OrdinalIgnoreCase;
            private static readonly StringComparison KeyComparison = StringComparison.OrdinalIgnoreCase;

            private readonly string FileName;
            private readonly string LineEnding;
            private readonly List<LineData> Lines = new List<LineData>();

            public PropertiesFile(string fileName, bool isOverride = false) {
                this.FileName = fileName;

                string fileContent = null;
                try {
                    fileContent = File.ReadAllText(fileName, Utf8);
                } catch (IOException) {
                } catch (UnauthorizedAccessException) { }

                string lineEnding = null;
                if (fileContent != null) {
                    var currLine = new StringBuilder();
                    var lineEndingDetermined = false;

                    char prevChar = '\0';
                    foreach (var ch in fileContent) {
                        if (ch == '\n') {
                            if (prevChar == '\r') { //CRLF pair
                                if (!lineEndingDetermined) {
                                    lineEnding = "\r\n";
                                    lineEndingDetermined = true;
                                }
                            } else {
                                if (!lineEndingDetermined) {
                                    lineEnding = "\n";
                                    lineEndingDetermined = true;
                                }
                                processLine(currLine);
                                currLine.Clear();
                            }
                        } else if (ch == '\r') {
                            processLine(currLine);
                            if (!lineEndingDetermined) { lineEnding = "\r"; } //do not set as determined as there is possibility of trailing LF
                        } else {
                            if (lineEnding != null) { lineEndingDetermined = true; } //if there was a line ending before, mark it as determined
                            currLine.Append(ch);
                        }
                        prevChar = ch;
                    }
                    this.FileExists = true;

                    processLine(currLine);
                }
                this.LineEnding = lineEnding ?? Environment.NewLine;

                void processLine(StringBuilder line) {
                    var lineText = line.ToString();
                    line.Clear();

                    char? valueSeparator = null;

                    var sbKey = new StringBuilder();
                    var sbValue = new StringBuilder();
                    var sbComment = new StringBuilder();
                    var sbWhitespace = new StringBuilder();
                    var sbEscapeLong = new StringBuilder();
                    string separatorPrefix = null;
                    string separatorSuffix = null;
                    string commentPrefix = null;

                    var state = State.Default;
                    var prevState = State.Default;
                    foreach (var ch in lineText) {
                        switch (state) {
                            case State.Default:
                                if (char.IsWhiteSpace(ch)) {
                                } else if (ch == '#') {
                                    sbComment.Append(ch);
                                    state = State.Comment;
                                } else if (ch == '\\') {
                                    state = State.KeyEscape;
                                } else {
                                    sbKey.Append(ch);
                                    state = State.Key;
                                }
                                break;

                            case State.Comment:
                                sbComment.Append(ch);
                                break;

                            case State.Key:
                                if (char.IsWhiteSpace(ch)) {
                                    valueSeparator = ch;
                                    state = State.SeparatorOrValue;
                                } else if ((ch == ':') || (ch == '=')) {
                                    valueSeparator = ch;
                                    state = State.ValueOrWhitespace;
                                } else if (ch == '#') {
                                    sbComment.Append(ch);
                                    state = State.Comment;
                                } else if (ch == '\\') {
                                    state = State.KeyEscape;
                                } else {
                                    sbKey.Append(ch);
                                }
                                break;

                            case State.SeparatorOrValue:
                                if (char.IsWhiteSpace(ch)) {
                                } else if ((ch == ':') || (ch == '=')) {
                                    valueSeparator = ch;
                                    state = State.ValueOrWhitespace;
                                } else if (ch == '#') {
                                    sbComment.Append(ch);
                                    state = State.Comment;
                                } else if (ch == '\\') {
                                    state = State.ValueEscape;
                                } else {
                                    sbValue.Append(ch);
                                    state = State.Value;
                                }
                                break;

                            case State.ValueOrWhitespace:
                                if (char.IsWhiteSpace(ch)) {
                                } else if (ch == '#') {
                                    sbComment.Append(ch);
                                    state = State.Comment;
                                } else if (ch == '\\') {
                                    state = State.ValueEscape;
                                } else {
                                    sbValue.Append(ch);
                                    state = State.Value;
                                }
                                break;

                            case State.Value:
                                if (char.IsWhiteSpace(ch)) {
                                    state = State.ValueOrComment;
                                } else if (ch == '#') {
                                    sbComment.Append(ch);
                                    state = State.Comment;
                                } else if (ch == '\\') {
                                    state = State.ValueEscape;
                                } else {
                                    sbValue.Append(ch);
                                }
                                break;

                            case State.ValueOrComment:
                                if (char.IsWhiteSpace(ch)) {
                                } else if (ch == '#') {
                                    sbComment.Append(ch);
                                    state = State.Comment;
                                } else if (ch == '\\') {
                                    sbValue.Append(sbWhitespace);
                                    state = State.ValueEscape;
                                } else {
                                    sbValue.Append(sbWhitespace);
                                    sbValue.Append(ch);
                                    state = State.Value;
                                }
                                break;

                            case State.KeyEscape:
                            case State.ValueEscape:
                                if (ch == 'u') {
                                    state = (state == State.KeyEscape) ? State.KeyEscapeLong : State.ValueEscapeLong;
                                } else {
                                    char newCh;
                                    switch (ch) {
                                        case '0': newCh = '\0'; break;
                                        case 'b': newCh = '\b'; break;
                                        case 't': newCh = '\t'; break;
                                        case 'n': newCh = '\n'; break;
                                        case 'r': newCh = '\r'; break;
                                        case '_': newCh = ' '; break;
                                        default: newCh = ch; break;
                                    }
                                    if (state == State.KeyEscape) {
                                        sbKey.Append(newCh);
                                    } else {
                                        sbValue.Append(newCh);
                                    }
                                    state = (state == State.KeyEscape) ? State.Key : State.Value;
                                }
                                break;

                            case State.KeyEscapeLong:
                            case State.ValueEscapeLong:
                                sbEscapeLong.Append(ch);
                                if (sbEscapeLong.Length == 4) {
                                    if (int.TryParse(sbEscapeLong.ToString(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var chValue)) {
                                        if (state == State.KeyEscape) {
                                            sbKey.Append((char)chValue);
                                        } else {
                                            sbValue.Append((char)chValue);
                                        }
                                    }
                                    state = (state == State.KeyEscapeLong) ? State.Key : State.Value;
                                }
                                break;
                        }

                        if (char.IsWhiteSpace(ch) && (prevState != State.KeyEscape) && (prevState != State.ValueEscape) && (prevState != State.KeyEscapeLong) && (prevState != State.ValueEscapeLong)) {
                            sbWhitespace.Append(ch);
                        } else if (state != prevState) { //on state change, clean comment prefix
                            if ((state == State.ValueOrWhitespace) && (separatorPrefix == null)) {
                                separatorPrefix = sbWhitespace.ToString();
                                sbWhitespace.Clear();
                            } else if ((state == State.Value) && (separatorSuffix == null)) {
                                separatorSuffix = sbWhitespace.ToString();
                                sbWhitespace.Clear();
                            } else if ((state == State.Comment) && (commentPrefix == null)) {
                                commentPrefix = sbWhitespace.ToString();
                                sbWhitespace.Clear();
                            } else if ((state == State.Key) || (state == State.ValueOrWhitespace) || (state == State.Value)) {
                                sbWhitespace.Clear();
                            }
                        }

                        prevState = state;
                    }

                    this.Lines.Add(new LineData(sbKey.ToString(), separatorPrefix, valueSeparator, separatorSuffix, sbValue.ToString(), commentPrefix, sbComment.ToString()));
                }

#if DEBUG
                foreach (var line in this.Lines) {
                    if (!string.IsNullOrEmpty(line.Key)) {
                        Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "[Settings] {0}{2}: {1}", line.Key, line.Value, (isOverride ? "*" : "")));
                    }
                }
#endif
            }

            public bool FileExists { get; } //false if there was an error during load

            public bool Save() {
                string fileContent = string.Join(this.LineEnding, this.Lines);
                try {
                    var directoryPath = Path.GetDirectoryName(this.FileName);
                    if (!Directory.Exists(directoryPath)) {
                        var directoryStack = new Stack<string>();
                        do {
                            directoryStack.Push(directoryPath);
                            directoryPath = Path.GetDirectoryName(directoryPath);
                        } while (!Directory.Exists(directoryPath));

                        while (directoryStack.Count > 0) {
                            try {
                                Directory.CreateDirectory(directoryStack.Pop());
                            } catch (IOException) {
                                break;
                            } catch (UnauthorizedAccessException) {
                                break;
                            }
                        }
                    }

                    File.WriteAllText(this.FileName, fileContent, Utf8);
                    return true;
                } catch (IOException) {
                    return false;
                } catch (UnauthorizedAccessException) {
                    return false;
                }
            }


            private enum State {
                Default,
                Comment,
                Key,
                KeyEscape,
                KeyEscapeLong,
                SeparatorOrValue,
                ValueOrWhitespace,
                Value,
                ValueEscape,
                ValueEscapeLong,
                ValueOrComment,
            }


            private class LineData {
                public LineData()
                    : this(null, null, null, null, null, null, null) {
                }
                public LineData(LineData template, string key, string value)
                    : this(key,
                          template?.SeparatorPrefix ?? "",
                          template?.Separator ?? ':',
                          template?.SeparatorSuffix ?? " ",
                          value,
                          null, null) {
                    if (template != null) {
                        var firstKeyTotalLength = (template.Key?.Length ?? 0) + (template.SeparatorPrefix?.Length ?? 0) + 1 + (template.SeparatorSuffix?.Length ?? 0);
                        var totalLengthWithoutSuffix = key.Length + (template.SeparatorPrefix?.Length ?? 0) + 1;
                        var maxSuffixLength = firstKeyTotalLength - totalLengthWithoutSuffix;
                        if (maxSuffixLength < 1) { maxSuffixLength = 1; } //leave at least one space
                        if (this.SeparatorSuffix.Length > maxSuffixLength) {
                            this.SeparatorSuffix = this.SeparatorSuffix.Substring(0, maxSuffixLength);
                        }
                    }
                }

                public LineData(string key, string separatorPrefix, char? separator, string separatorSuffix, string value, string commentPrefix, string comment) {
                    this.Key = key;
                    this.SeparatorPrefix = separatorPrefix;
                    this.Separator = separator ?? ':';
                    this.SeparatorSuffix = separatorSuffix;
                    this.Value = value;
                    this.CommentPrefix = commentPrefix;
                    this.Comment = comment;
                }

                public string Key { get; set; }
                public string SeparatorPrefix { get; set; }
                public char Separator { get; }
                public string SeparatorSuffix { get; set; }
                public string Value { get; set; }
                public string CommentPrefix { get; }
                public string Comment { get; }

                public override string ToString() {
                    var sb = new StringBuilder();
                    if (!string.IsNullOrEmpty(this.Key)) {
                        EscapeIntoStringBuilder(sb, this.Key, isKey: true);

                        if (!string.IsNullOrEmpty(this.Value)) {
                            if ((this.Separator == ':') || (this.Separator == '=')) {
                                sb.Append(this.SeparatorPrefix);
                                sb.Append(this.Separator);
                                sb.Append(this.SeparatorSuffix);
                            } else {
                                sb.Append(string.IsNullOrEmpty(this.SeparatorSuffix) ? " " : this.SeparatorSuffix);
                            }
                            EscapeIntoStringBuilder(sb, this.Value ?? "");
                        } else { //try to preserve formatting in case of spaces (thus omitted)
                            sb.Append(this.SeparatorPrefix);
                            switch (this.Separator) {
                                case ':': sb.Append(":"); break;
                                case '=': sb.Append("="); break;
                            }
                            sb.Append(this.SeparatorSuffix);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.Comment)) {
                        if (!string.IsNullOrEmpty(this.CommentPrefix)) { sb.Append(this.CommentPrefix); }
                        sb.Append(this.Comment);
                    }

                    return sb.ToString();
                }

                private static void EscapeIntoStringBuilder(StringBuilder sb, string text, bool isKey = false) {
                    for (int i = 0; i < text.Length; i++) {
                        var ch = text[i];
                        switch (ch) {
                            case '\\': sb.Append(@"\\"); break;
                            case '\0': sb.Append(@"\0"); break;
                            case '\b': sb.Append(@"\b"); break;
                            case '\t': sb.Append(@"\t"); break;
                            case '\r': sb.Append(@"\r"); break;
                            case '\n': sb.Append(@"\n"); break;
                            case '#': sb.Append(@"\#"); break;
                            default:
                                if (char.IsControl(ch)) {
                                    sb.Append(((int)ch).ToString("X4", CultureInfo.InvariantCulture));
                                } else if (ch == ' ') {
                                    if ((i == 0) || (i == (text.Length - 1)) || isKey) {
                                        sb.Append(@"\_");
                                    } else {
                                        sb.Append(ch);
                                    }
                                } else if (char.IsWhiteSpace(ch)) {
                                    switch (ch) {
                                        case '\0': sb.Append(@"\0"); break;
                                        case '\b': sb.Append(@"\b"); break;
                                        case '\t': sb.Append(@"\t"); break;
                                        case '\n': sb.Append(@"\n"); break;
                                        case '\r': sb.Append(@"\r"); break;
                                        default: sb.Append(((int)ch).ToString("X4", CultureInfo.InvariantCulture)); break;
                                    }
                                } else if (ch == '\\') {
                                    sb.Append(@"\\");
                                } else {
                                    sb.Append(ch);
                                }
                                break;
                        }
                    }
                }

                public bool IsEmpty => string.IsNullOrEmpty(this.Key) && string.IsNullOrEmpty(this.Value) && string.IsNullOrEmpty(this.CommentPrefix) && string.IsNullOrEmpty(this.Comment);

            }


            private Dictionary<string, int> CachedEntries;
            private void FillCache() {
                this.CachedEntries = new Dictionary<string, int>(KeyComparer);
                for (var i = 0; i < this.Lines.Count; i++) {
                    var line = this.Lines[i];
                    if (!line.IsEmpty) {
                        if (this.CachedEntries.ContainsKey(line.Key)) {
                            this.CachedEntries[line.Key] = i; //last key takes precedence
                        } else {
                            this.CachedEntries.Add(line.Key, i);
                        }
                    }
                }
            }


            public string ReadOne(string key) {
                if (this.CachedEntries == null) { FillCache(); }

                return this.CachedEntries.TryGetValue(key, out var lineNumber) ? this.Lines[lineNumber].Value : null;
            }

            public IEnumerable<string> ReadMany(string key) {
                if (this.CachedEntries == null) { FillCache(); }

                foreach (var line in this.Lines) {
                    if (string.Equals(key, line.Key, KeyComparison)) {
                        yield return line.Value;
                    }
                }
            }


            public void WriteOne(string key, string value) {
                if (this.CachedEntries == null) { FillCache(); }

                if (this.CachedEntries.TryGetValue(key, out var lineIndex)) {
                    var data = this.Lines[lineIndex];
                    data.Key = key;
                    data.Value = value;
                } else {
                    var hasLines = (this.Lines.Count > 0);
                    var newData = new LineData(hasLines ? this.Lines[0] : null, key, value);
                    if (!hasLines) {
                        this.CachedEntries.Add(key, this.Lines.Count);
                        this.Lines.Add(newData);
                        this.Lines.Add(new LineData());
                    } else if (!this.Lines[this.Lines.Count - 1].IsEmpty) {
                        this.CachedEntries.Add(key, this.Lines.Count);
                        this.Lines.Add(newData);
                    } else {
                        this.CachedEntries.Add(key, this.Lines.Count - 1);
                        this.Lines.Insert(this.Lines.Count - 1, newData);
                    }
                }
            }

            public void WriteMany(string key, IEnumerable<string> values) {
                if (this.CachedEntries == null) { FillCache(); }

                if (this.CachedEntries.TryGetValue(key, out var lineIndex)) {
                    int lastIndex = 0;
                    LineData lastLine = null;
                    for (var i = this.Lines.Count - 1; i >= 0; i--) { //find insertion point
                        var line = this.Lines[i];
                        if (string.Equals(key, line.Key, KeyComparison)) {
                            if (lastLine == null) {
                                lastLine = line;
                                lastIndex = i;
                            } else {
                                lastIndex--;
                            }
                            this.Lines.RemoveAt(i);
                        }
                    }

                    var hasLines = (this.Lines.Count > 0);
                    foreach (var value in values) {
                        this.Lines.Insert(lastIndex, new LineData(lastLine ?? (hasLines ? this.Lines[0] : null), key, value));
                        lastIndex++;
                    }

                    FillCache();
                } else {
                    var hasLines = (this.Lines.Count > 0);
                    if (!hasLines) {
                        foreach (var value in values) {
                            this.CachedEntries[key] = this.Lines.Count;
                            this.Lines.Add(new LineData(null, key, value));
                        }
                        this.Lines.Add(new LineData());
                    } else if (!this.Lines[this.Lines.Count - 1].IsEmpty) {
                        foreach (var value in values) {
                            this.CachedEntries[key] = this.Lines.Count;
                            this.Lines.Add(new LineData(this.Lines[0], key, value));
                        }
                    } else {
                        foreach (var value in values) {
                            this.CachedEntries[key] = this.Lines.Count - 1;
                            this.Lines.Insert(this.Lines.Count - 1, new LineData(this.Lines[0], key, value));
                        }
                    }
                }
            }


            public void Delete(string key) {
                if (this.CachedEntries == null) { FillCache(); }

                this.CachedEntries.Remove(key);
                for (var i = this.Lines.Count - 1; i >= 0; i--) {
                    var line = this.Lines[i];
                    if (string.Equals(key, line.Key, KeyComparison)) {
                        this.Lines.RemoveAt(i);
                    }
                }
            }

            public void DeleteAll() {
                this.Lines.Clear();
                this.FillCache();
            }

        }

        #endregion

        #endregion

    }
}
