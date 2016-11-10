using UnityEngine;
using System.Collections;
using System.IO;
public class FileUtils  {

	private static FileUtils _instance;
	public static FileUtils Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new FileUtils();
			}
			return _instance;
		}
	}

	public void deleteFile(string path)
	{
		if (File.Exists (path)) {
			File.Delete (path);
		}
	}

	public bool isExisitFile(string path)
	{
		return File.Exists (path);
	}
	public string readFileByString(string path)
	{
		string s = System.IO.File.ReadAllText (@path);
		return s;
	}

	public byte[] readFileByBytes(string path)
	{
		byte[] bytes = System.IO.File.ReadAllBytes (@path);
		return bytes;
	}
	
	public FileStream getFile(string filePath,FileMode mode = FileMode.Create)
	{

		string floderPath = filePath.Substring(0,filePath.LastIndexOf("/"));
		if (Directory.Exists(floderPath) == false)//如果不存在就创建file文件夹
		{
			Directory.CreateDirectory(floderPath);
		}
		if (mode == FileMode.Append) {
			FileStream donwload_f = new FileStream(filePath,FileMode.Append);
			return donwload_f;
		}
		if (File.Exists (filePath)) {
			File.Delete (filePath);
		}
		FileStream f = new FileStream (filePath, FileMode.Create);
		return f;
	}

	public void close(FileStream f)
	{
		if (f != null) {
			f.Close();
			f = null;
		}
	}


}
