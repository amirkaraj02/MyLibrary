using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLibrary.App_Classes
{
    public class libraryDb
	{
		private static db_LibraryEntities connection;
		public static db_LibraryEntities Connection
		{
			get 
			{
				if(connection == null)
				{
					connection = new db_LibraryEntities();
				}
				return connection;
			}
			set { connection = value; }
		}


	}
}