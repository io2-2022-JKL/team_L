using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdfSharp.Fonts;
using System.Reflection;
using System.IO;

namespace VaccinationSystem.Services
{
    class MyFontResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            var name = familyName.ToLower().TrimEnd('#');

            switch (name)
            {
                case "arial":
                    if (isBold)
                    {
                        if (isItalic)
                            return new FontResolverInfo("Arial#bi");
                        return new FontResolverInfo("Arial#b");
                    }
                    if (isItalic)
                        return new FontResolverInfo("Arial#i");
                    return new FontResolverInfo("Arial#");
            }
            return PlatformFontResolver.ResolveTypeface(familyName, isBold, isItalic);
        }

        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "Arial#":
                    return LoadFontData("VaccinationSystem.fonts.arial.arial.ttf"); ;

                case "Arial#b":
                    return LoadFontData("VaccinationSystem.fonts.arial.arialbd.ttf"); ;

                case "Arial#i":
                    return LoadFontData("VaccinationSystem.fonts.arial.ariali.ttf");

                case "Arial#bi":
                    return LoadFontData("VaccinationSystem.fonts.arial.arialbi.ttf");
            }

            return null;
        }

        private byte[] LoadFontData(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();


            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                if (stream == null)
                    throw new ArgumentException("No resource with name " + name);

                int count = (int)stream.Length;
                byte[] data = new byte[count];
                stream.Read(data, 0, count);
                return data;
            }
        }

        internal static MyFontResolver OurGlobalFontResolver = null;

        internal static void Apply()
        {
            if (OurGlobalFontResolver == null || GlobalFontSettings.FontResolver == null)
            {
                if (OurGlobalFontResolver == null)
                    OurGlobalFontResolver = new MyFontResolver();

                GlobalFontSettings.FontResolver = OurGlobalFontResolver;
            }
        }
    }
}
