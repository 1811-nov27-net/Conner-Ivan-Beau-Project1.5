﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VaporWebSite.App.Controllers
{
    public class DlcController : ARequestController
    {

        public DlcController(HttpClient client) : base (client)
        {

        }

        // GET: Dlc
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: Dlc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dlc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dlc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dlc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dlc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dlc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dlc/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}