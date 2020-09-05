using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Save_Editor {
    public static class Extensions {
        public static T GetDataAs<T>(this IEnumerable<byte> bytes) {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try {
                return (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            } finally {
                if (handle.IsAllocated) {
                    handle.Free();
                }
            }
        }

        public static byte[] GetBytes<T>(this T @struct) {
            var size   = Marshal.SizeOf(@struct);
            var bytes  = new byte[size];
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);

            try {
                Marshal.StructureToPtr(@struct, handle.AddrOfPinnedObject(), false);
                return bytes;
            } finally {
                if (handle.IsAllocated) {
                    handle.Free();
                }
            }
        }

        public static void Seek(this Stream stream, long offset) {
            stream.Seek(offset, SeekOrigin.Current);
        }

        public static List<byte> ReadRemainderAsByteArray(this BinaryReader reader) {
            var list = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length) {
                list.Add(reader.ReadByte());
            }
            return list;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T> {
            if (val.CompareTo(min) < 0) {
                return min;
            } else if (val.CompareTo(max) > 0) {
                return max;
            } else {
                return val;
            }
        }

        public static V TryGet<K, V>(this IDictionary<K, V> dict, K key, V defaultValue) {
            if (dict == null) return defaultValue;
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        public static string TryGet<K>(this IDictionary<K, string> dict, K key, string defaultValue = "Unknown") {
            if (dict == null) return defaultValue;
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        public static IEnumerable<List<T>> ChunkIntoToLists<T>(this IEnumerable<T> items, int splitSize) {
            var list = new List<T>();

            foreach (var item in items) {
                if (list.Count == splitSize) {
                    yield return list;
                    list = new List<T>();
                }
                list.Add(item);
            }

            if (list.Count > 0) yield return list;
        }
    }
}