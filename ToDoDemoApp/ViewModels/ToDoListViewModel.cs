using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoDemoApp.Helpers;
using Dapper;
using System.Data.SqlClient;
using ToDoDemoApp.Models;

namespace ToDoDemoApp.ViewModels
{
    public class ToDoListViewModel
    {
        public ToDoListViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.ListItemEntity = new ListItem();
                this.ToDoItems = db.Query<ListItem>("SELECT * from ListItems ORDER BY AddDateTime DESC").ToList();
            }
        }
        /**
         * List of all items 
         */
        public List<ListItem> ToDoItems { get; set; }
        /**
         * Editable ToDo list entity
         */
        public ListItem ListItemEntity { get; set; }
    }
}
