using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LAeq_and_LEX.Models;
using System.IO;
using System.Text;
using System.Net;
using System.Data;

namespace LAeq_and_LEX.Controllers
{
    public class SettingController : Controller
    {
        private ehslaeq db = new ehslaeq();
        // GET: Setting/Get
        public ActionResult Get()
        {
            List<setting> items = db.settings.ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        // GET: Setting/GetByIndex/5
        public ActionResult GetByIndex(int id)
        {
            setting item = db.settings.Find(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        // GET: Setting/GetByIndex/5
        public ActionResult GetResult(string eq, string start, string end, string sets)
        {
            try
            {
                if (!string.IsNullOrEmpty(sets))
                {
                    List<setting> settingitems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<setting>>(sets);
                    DateTime sd = Convert.ToDateTime(start);
                    DateTime ed = Convert.ToDateTime(end);
                    List<laeqresult> result = new List<laeqresult>();
                    List<serverData> rawData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<serverData>>(GetWebApi(eq, start, end));
                    if (rawData.Count > 0) {
                        foreach (setting item in settingitems)
                        {
                            if (Convert.ToDouble(item.start_time.Substring(0, item.start_time.IndexOf(':'))) < Convert.ToDouble(item.end_time.Substring(0, item.start_time.IndexOf(':'))))
                            {
                                for (DateTime newdate = sd; newdate < ed; newdate = newdate.AddDays(1))
                                {
                                    laeqresult tmp = new laeqresult
                                    {
                                        during = item.during,
                                        interval = item.interval,
                                        start_time = newdate.ToString("yyyy-MM-dd") + " " + item.start_time,
                                        end_time = newdate.ToString("yyyy-MM-dd") + " " + item.end_time,
                                    };
                                    result.Add(tmp);
                                }
                            }
                            else
                            {
                                for (DateTime newdate = sd; newdate < ed.AddDays(-1); newdate = newdate.AddDays(1))
                                {
                                    laeqresult tmp = new laeqresult
                                    {
                                        during = item.during,
                                        interval = item.interval,
                                        start_time = newdate.ToString("yyyy-MM-dd") + " " + item.start_time,
                                        end_time = newdate.AddDays(1).ToString("yyyy-MM-dd") + " " + item.end_time,
                                    };
                                    result.Add(tmp);
                                }
                            }
                        }
                        foreach (laeqresult item in result) {
                           List<string> query = (from i in rawData
                                                where Convert.ToDateTime(i.dtime) > Convert.ToDateTime(item.start_time) &&
                                                Convert.ToDateTime(i.dtime) < Convert.ToDateTime(item.end_time)
                                                select  i.data).ToList() ;
                            double tmp0 = Calculate(item, query);
                            item.laeq = tmp0 == 0 ? 0 : Math.Round(tmp0, 2);
                            item.lex = tmp0 == 0 ? 0 : Math.Round(tmp0 + 10 * Math.Log(Convert.ToDouble(item.during) / 8, 10),2);
                        }
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else {
                        return new HttpStatusCodeResult(204,"没有该时间段的数据....");
                    }
                }
                else
                {
                    return new HttpStatusCodeResult(204,"请选择时间段...");
                }
            }
            catch (Exception err) {
                throw new Exception(err.Message);
            }
        }

        // POST: Setting/Create
        [HttpPost]
        public ActionResult Create(setting item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.interval.ToString()))
                {
                    db.settings.Add(item);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Add Setting Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204);
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        // POST: Setting/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, setting item)
        {
            try
            {
                if (!string.IsNullOrEmpty(item.interval.ToString()))
                {
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Change Setting Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204);
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        // GET: Setting/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                setting item = db.settings.Find(id);
                if (item != null)
                {
                    db.settings.Attach(item);
                    db.settings.Remove(item);
                    db.SaveChanges();
                    return new HttpStatusCodeResult(200, "Delete Setting Success");
                }
                else
                {
                    return new HttpStatusCodeResult(204);
                }
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public string GetWebApi(string eq ,string start, string end) {
            string Url = "https://env.jingkongyun.com:9089/api/sensorData/getDataByDeviceidAndTime?deviceid=" + eq + "&start=" + start + "&end=" + end;
            string s2 = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "JSON";
                request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader reader = new StreamReader(s);
                s2 = reader.ReadToEnd();
                s.Close();
            }
            catch (Exception e)
            {
                s2 = e.Message;
            }
            return s2;
        }
        public double Calculate(laeqresult paras, List<string> items)
        {
            if (items.Count > 0)
            {
                double sum = 0;
                foreach (string i in items)
                {
                    sum += Math.Pow(10, 0.1 * Convert.ToDouble(i));
                }
                double result = 10 * Math.Log(sum * Convert.ToDouble(paras.interval) / 60 / Convert.ToDouble(paras.during), 10);
                return result;
            }
            else {
                return 0;
            }
        }
    }
}
