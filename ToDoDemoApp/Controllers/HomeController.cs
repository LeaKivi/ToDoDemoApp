using System;
using System.Linq;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc;
using ToDoDemoApp.Helpers;
using ToDoDemoApp.Models;
using ToDoDemoApp.ViewModels;

namespace ToDoDemoApp.Controllers
{
    public class HomeController : Controller
    {
        /**
         * Deliver the view for the ToDo list
         */
        public IActionResult Index()
        {
            ToDoListViewModel viewModel = new ToDoListViewModel();
            return View("Index", viewModel);
        }

        /**
         * Creates or updates the  List Item
         * <param name="viewModel">Data recieved from the form</param>
         */
        public IActionResult CreateUpdate(ToDoListViewModel viewModel)
        {
            //check for validation errors
            if (ModelState.IsValid)
            {
                var listItem = viewModel.ListItemEntity;
                using (var db = DbHelper.GetConnection())
                {
                    //new items are added with Id 0
                    if (listItem.Id <= 0)
                    {
                        listItem.AddDateTime = DateTime.Now;
                        try 
                        {
                            db.Insert<ListItem>(listItem); 
                        }
                        catch(Exception e)
                        {
                            var message = e.Message;                           
                        }                                               
                    }
                    else
                    {
                        ListItem dbItem = db.Get<ListItem>(listItem.Id);
                        dbItem.AddDateTime = DateTime.Now;
                        dbItem.Title = listItem.Title;
                        try
                        {
                            var result = TryUpdateModelAsync<ListItem>(dbItem, "ListItemEntity");
                            db.Update<ListItem>(dbItem);
                        }
                        catch (Exception e)
                        {
                            var message = e.Message;                            
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new ToDoListViewModel());
        }

        /**
         * Edit the List Item
         * <param name="id">Id of the required list item</param>
         */
        public IActionResult Edit(int id)
        {
            ToDoListViewModel viewModel = new ToDoListViewModel();
            
            //Select the required item based on Ids
            viewModel.ListItemEntity = viewModel.ToDoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

       /**
       * Remove the ListItem from the list
       * <param name="id">Id of the required list item</param>
       */
        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ListItem item = db.Get<ListItem>(id);
                if (item != null)
                    db.Delete(item);
                return RedirectToAction("Index");
            }
        }

        /**
        * Mark the ListItem as done in the list
        * <param name="id">Id of the required list item</param>
        */
        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ListItem item = db.Get<ListItem>(id);
                if (item != null)
                {
                    item.IsDone = !item.IsDone;
                    try
                    {
                        db.Update<ListItem>(item);
                    }
                    catch (Exception e)
                    {
                        var message = e.Message;                        
                    }
                }
                return RedirectToAction("Index");
            }
        }
    }
}