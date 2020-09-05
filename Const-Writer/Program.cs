using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Save_Editor;

namespace Const_Writer {
    public static class Program {
        private const string RESOURCES_ROOT = @"..\..\..\Resources";
        private const string NAMESPACE      = "Save_Editor.Resources";

        [STAThread]
        public static void Main() {
            WriteResult(RESOURCES_ROOT, "ItemConst", new ValueClassTemplate {
                Session = new Dictionary<string, object> {
                    {"_namespace", NAMESPACE},
                    {"className", "ItemConst"}, {
                        "valueDataPairs", from kvp in Data.ITEM_NAMES_BY_ID
                                          select new Tuple<string, int>(Regex.Replace(kvp.Value.Replace("+", "Plus"), @"[^\w\d]+", "_"), kvp.Key)
                    }
                }
            });

            WriteResult(RESOURCES_ROOT, "SkillConst", new ValueClassTemplate {
                Session = new Dictionary<string, object> {
                    {"_namespace", NAMESPACE},
                    {"className", "SkillConst"}, {
                        "valueDataPairs", from kvp in Data.SKILL_NAMES_BY_ID
                                          select new Tuple<string, int>(Regex.Replace(kvp.Value.Replace("+", "Plus"), @"[^\w\d]+", "_"), kvp.Key)
                    }
                }
            });
        }

        public static void WriteResult(string dir, string className, dynamic template, string ext = "cs") {
            template.Initialize();
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText($"{dir}\\{className}.{ext}", (string) template.TransformText());
        }
    }
}

namespace Save_Editor {
    public static class Program {
        public static string[] Split(this string str, string separator) {
            return str.Split(new[] {separator}, StringSplitOptions.None);
        }
    }
}