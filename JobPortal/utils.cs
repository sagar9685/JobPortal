using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobPortal
{
    public class utils
    {
        public static bool IsValidExtension(string fileName)
        {
            string[] fileExtensions = { ".jpg", ".jpeg", ".png" };
            foreach (string extension in fileExtensions)
            {
                if (fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Return true if valid
                }
            }
            return false; // Return false if no valid extension found
        }

        public static bool IsValidExtension4Resume(string fileName)
        {
            string[] fileExtensions = { ".doc", ".docx", ".pdf" };
            foreach (string extension in fileExtensions)
            {
                if (fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Return true if valid
                }
            }
            return false; // Return false if no valid extension found
        }
    }
}