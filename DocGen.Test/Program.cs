using System;
using System.Collections.Generic;
using DocGen.Abstract.Application.Builder;
using DocGen.Abstract.Constants;
using DocGen.Abstract.Interface.Content;
using DocGen.Abstract.Interface.FactorySettings;
using DocGen.Abstract.Settings.Concrete; // FontSettings
using DocGen.Pdf.Creator;
using DocGen.Pdf.FactorySettings;
using DocGen.Word.Creator;
using DocGen.Word.FactorySettings;

namespace mozCode.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1) Önce, base / default font settings oluşturalım.
            //    Burada basitçe FontSettings ile varsayılan değerler ayarlıyoruz.
            //    Bu ayarlar PDF veya Word tarafında yansıyacak.
            var defaultFontSettings = new FontSettings
            {
                FontColor = "#001133",
                FontSize = 12.0,
                FontBold = false,
                FontItalic = false,
                FontUnderline = false,
                Justification = "left"
            };

            // 2) Ardından PDF ve Word için ayrı FontSettingsFactory oluşturuyoruz.
            //    Bu, her iki çıktıda da defaultFontSettings baz alınarak
            //    format-spesifik font ayarları (örn. PdfFontSettings, WordFontSettings) üretir.
            IFontSettingsFactory pdfFontFactory = new PdfFontSettingsFactory(defaultFontSettings);
            IFontSettingsFactory wordFontFactory = new WordFontSettingsFactory(defaultFontSettings);

            // 3) DocumentBuilder yardımıyla bir DocumentContent oluşturalım.
            //    Header (Üst Bilgi), Footer (Alt Bilgi), Signature (İmza) ve BodySection ekliyoruz.
            var builder = new DocumentBuilder(pdfFontFactory)
                // Yukarıda PDF factory kullansak da builder'ı PDF/Word fark etmez. 
                // Sadece varsayılan factory'yi baz alır. 
                // Sonrasında "SetDefaultFontSettings" ile de aynı şekilde ayarlanabilir.
                .SetDefaultFontSettings(defaultFontSettings)

                // HEADER (üst bilgi)
                .Header(h =>
                {
                    // Örnek tablo satırı ekleyelim:
                    var row = new DocGen.Abstract.Domain.Table.TablesRow();
                    row.ColumnWidths = new List<int> { 40, 60 };

                    // 1. Hücre (sol taraf)
                    var cellLeft = new DocGen.Abstract.Domain.Table.TableCellText
                    {
                        Content = "Firma Logo",
                        FontSettings = new FontSettings
                        {
                            FontColor = "#AA0000",
                            FontSize = 10,
                            FontBold = true
                        }
                    };
                    row.TableCells.Add(cellLeft);

                    // 2. Hücre (sağ taraf)
                    var cellRight = new DocGen.Abstract.Domain.Table.TableCellText
                    {
                        Content = "Üst Bilgi Yazısı",
                        FontSettings = new FontSettings
                        {
                            FontColor = "#222222",
                            FontSize = 10,
                            Justification = "right"
                        }
                    };
                    row.TableCells.Add(cellRight);

                    h.AddRow(row);
                })

                // BODY (gövde içeriği) -> BodySection
                .BodySection(
                    title: "Giriş",
                    richTextContent: "Burası **kalın** metin, burası da _italik_ metin. \n İkinci satır.",
                    hierarchyLevel: 1,
                    sectionNotes: new List<string> { "Dipnot veya kısa açıklama." },
                    numberingType: TitleNumberingType.Numeric
                )
                .BodySection(
                    title: "Detaylı Bilgi",
                    richTextContent: "Burada __altçizili__ bir örnek var.\nBir alt paragraf daha.",
                    hierarchyLevel: 2,
                    sectionNotes: new List<string> { "Ek açıklamalar..." },
                    numberingType: TitleNumberingType.Numeric
                )

                // FOOTER (alt bilgi)
                .Footer(f =>
                {
                    var row = new DocGen.Abstract.Domain.Table.TablesRow();
                    row.ColumnWidths = new List<int> { 100 }; // tek sütun
                    var cell = new DocGen.Abstract.Domain.Table.TableCellText
                    {
                        Content = "Sayfa Alt Bilgisi - " + DateTime.Now.ToShortDateString(),
                        FontSettings = new FontSettings
                        {
                            FontColor = "#000000",
                            FontSize = 10,
                            Justification = "center"
                        }
                    };
                    row.TableCells.Add(cell);

                    f.AddRow(row);
                })

                // SIGN (imza bölümü)
                .Signature(s =>
                {
                    var signRow = new DocGen.Abstract.Domain.Table.TablesRow();
                    signRow.ColumnWidths = new List<int> { 50, 50 };

                    var leftCell = new DocGen.Abstract.Domain.Table.TableCellText
                    {
                        Content = "Hazırlayan\n(İmza)",
                        FontSettings = new FontSettings
                        {
                            FontSize = 11,
                            FontBold = true,
                            Justification = "center"
                        }
                    };
                    signRow.TableCells.Add(leftCell);

                    var rightCell = new DocGen.Abstract.Domain.Table.TableCellText
                    {
                        Content = "Kontrol Eden\n(İmza)",
                        FontSettings = new FontSettings
                        {
                            FontSize = 11,
                            FontBold = true,
                            Justification = "center"
                        }
                    };
                    signRow.TableCells.Add(rightCell);

                    s.AddRow(signRow);
                });

            // 4) Artık builder'dan bir IDocumentContent üretebiliriz.
            var docContent = builder.Build();

            // 5) Hem PDF hem Word olarak oluşturalım.
            //    Seçenek olarak, sayfayı yatay (landscape) yapabilir veya arka plan resmi ekleyebiliriz.
            var options = new DocumentCreateOptions
            {
                IsLandscape = false,
                AddBackgroundImage = false,
                BackgroundImagePath = null
            };

            // 5a) PDF oluşturma
            var pdfCreator = new PdfDocumentCreator();
            pdfCreator.CreateDocument(docContent, options);
            Console.WriteLine("PDF oluşturuldu. (Masaüstüne CompleteSamplePdf.pdf kaydedildi)");

            // 5b) Word oluşturma
            // Word için, isterseniz docContent'i Word'e daha özel bir factory ile de inşa edebilirdiniz.
            // Fakat docContent zaten format bağımsız, tekrar kullanılabilir.
            var wordCreator = new WordDocumentCreator();
            wordCreator.CreateDocument(docContent, options);
            Console.WriteLine("Word oluşturuldu. (Masaüstüne CompleteSampleWord.docx kaydedildi)");

            Console.WriteLine("Tamamlandı. Çıkmak için bir tuşa basınız...");
            Console.ReadKey();
        }
    }
}
