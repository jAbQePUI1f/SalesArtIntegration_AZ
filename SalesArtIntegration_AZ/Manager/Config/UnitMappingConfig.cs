using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SalesArtIntegration_AZ.Manager.Config
{
    public class UnitMappingConfig
    {
        public Dictionary<string, string> Mapping { get; set; }
    }

    public static class BirimYoneticisi
    {
        private static UnitMappingConfig _config = null;
        private static readonly object _lock = new object();
        // Resources klasöründen okumak için
        private static string _jsonDosyaYolu = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Resources",
            "unit_mapping.json"
        );

        /// <summary>
        /// JSON dosyasını yükler
        /// </summary>
        private static void Yukle()
        {
            lock (_lock)
            {
                if (_config != null) return; // Zaten yüklenmişse tekrar yükleme

                try
                {
                    if (File.Exists(_jsonDosyaYolu))
                    {
                        string jsonIcerik = File.ReadAllText(_jsonDosyaYolu);
                        _config = JsonConvert.DeserializeObject<UnitMappingConfig>(jsonIcerik);

                        if (_config == null || _config.Mapping == null)
                        {
                            _config = new UnitMappingConfig
                            {
                                Mapping = new Dictionary<string, string>()
                            };
                        }
                    }
                    else
                    {
                        throw new FileNotFoundException($"unit_mapping.json dosyası bulunamadı: {_jsonDosyaYolu}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"unit_mapping.json dosyası okunamadı: {ex.Message}", ex);
                }
            }
        }

        /// <summary>
        /// Verilen anahtara karşılık gelen birim değerini getirir
        /// </summary>
        /// <param name="anahtar">ERP'den gelen birim adı</param>
        /// <returns>Eşleşen birim değeri. Bulunamazsa aynı değer döner</returns>
        public static string BirimGetir(string anahtar)
        {
            if (_config == null)
            {
                Yukle();
            }

            if (string.IsNullOrEmpty(anahtar))
                return string.Empty;

            if (_config != null && _config.Mapping != null && _config.Mapping.ContainsKey(anahtar))
            {
                return _config.Mapping[anahtar];
            }

            return anahtar; // Eşleşme bulunamazsa gelen değeri olduğu gibi döndür
        }

        /// <summary>
        /// Test için - JSON dosyasının yolunu döndürür
        /// </summary>
        public static string DosyaYolunuGetir()
        {
            return _jsonDosyaYolu;
        }

        /// <summary>
        /// Test için - JSON dosyasının içeriğini döndürür
        /// </summary>
        public static string JsonIceriginiGetir()
        {
            if (File.Exists(_jsonDosyaYolu))
            {
                return File.ReadAllText(_jsonDosyaYolu);
            }
            return "Dosya bulunamadı!";
        }

        /// <summary>
        /// Config'i sıfırlar ve yeniden yükler
        /// </summary>
        public static void YenidenYukle()
        {
            lock (_lock)
            {
                _config = null;
                Yukle();
            }
        }
    }
}