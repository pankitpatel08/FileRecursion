using System;
using System.IO;
using System.Text;

namespace RecursionSolver
{
    public class OrbitoryOperation : IFileOperations
    {
        /// <summary>
        /// Clone the file with new name
        /// </summary>
        /// <param name="filePath"></param>
        public void CloneFile(string filePath)
        {
            try
            {
                string currentName = Path.GetFileNameWithoutExtension(filePath);
                string newName = currentName + "_" + DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                string newlocation = filePath.Replace(currentName, newName);
                File.Copy(filePath, newlocation); // Try to move
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// delete existing file
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// modify files with new text
        /// </summary>
        /// <param name="filePath"></param>
        public void ModifyFile(string filePath)
        {
            try
            {
                string ext = Path.GetExtension(filePath);
                switch (ext.ToUpper())
                {
                    case ".DOC":
                    case ".DOCX":
                        Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                        Microsoft.Office.Interop.Word.Document doc = app.Documents.Open(filePath);
                        object missing = System.Reflection.Missing.Value;
                        doc.Content.InsertAfter("This is Sample Text");
                        doc.Save();
                        doc.Close(ref missing);
                        app.Quit(ref missing);
                        break;
                    case ".TXT":
                        StringBuilder strBuilder = new StringBuilder();
                        strBuilder.Append("This is Sample Text to be added. \n".ToString());
                        strBuilder.Append("Welcome to Append feature of Text File. \n".ToString());
                        strBuilder.Append("Copyright @Pankit, 2019. \n".ToString());
                        FileStream fStream = new FileStream(filePath, FileMode.Append);
                        fStream.Close();
                        string input = string.Empty;
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            input = reader.ReadToEnd();
                        }
                        StreamWriter sWriter = new StreamWriter(filePath);
                        sWriter.Write(input + "\n" + strBuilder);
                        sWriter.Flush();
                        sWriter.Close();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// rename file with new name
        /// </summary>
        /// <param name="filePath"></param>
        public void RenameFile(string filePath)
        {
            try
            {
                string newName = "renamed_" + DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                string currentName = Path.GetFileNameWithoutExtension(filePath);
                string newlocation = filePath.Replace(currentName, newName);
                File.Move(filePath, newlocation); // Try to move
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
