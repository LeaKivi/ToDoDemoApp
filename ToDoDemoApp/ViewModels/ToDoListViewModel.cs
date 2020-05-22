using System.Collections.Generic;
using System.Linq;
using ToDoDemoApp.Helpers;
using Dapper;
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
