using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodolistWinforms
{
    class TodoItemAdapter
    {
        public async Task<BindingList<TodoItem>> GetItemList()
        {
            WSManager mgr = new WSManager();
            BindingList<TodoItem> ret;
            string uri = "https://localhost:44374/api/Todo/";

            ret = await mgr.HTTPGet<BindingList<TodoItem>>(uri);

            return ret;
        }

        public async Task<TodoItem> GetItem(int id)
        {
            WSManager mgr = new WSManager();
            TodoItem ret;
            string uri = "https://localhost:44374/api/Todo/2";

            ret = await mgr.HTTPGet<TodoItem>(uri);

            return ret;
        }

        public async Task<bool> PostItem(TodoItem ti)
        {
            string uri = "https://localhost:44374/api/Todo";
            WSManager mgr = new WSManager();
            return await mgr.HTTPPost(uri, ti);
        }

        public async Task<bool> UpdateItem(TodoItem ti)
        {
            string uri = "https://localhost:44374/api/Todo/" + ti.Id;
            WSManager mgr = new WSManager();
            return await mgr.HTTPPut(uri, ti);
        }

        public async Task<bool> DeleteItem(TodoItem ti)
        {
            string uri = "https://localhost:44374/api/Todo/" + ti.Id;
            WSManager mgr = new WSManager();
            return await mgr.HTTPDelete(uri, ti);
        }
    }
}
