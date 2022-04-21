using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Yande.Api
{
    public class WordHelper
    {
        public void CreateWordFile(InterfaceVersion list1, List<InterfaceMethod> list2, List<InterfaceRequestField> list3)
        {
            var ss = new List<InterfaceVersion>();
            ss.Add(list1);

            //去重操作
            var distinctList2 = list2.Distinct().ToList();
            var distinctList3 = list3.Distinct().ToList();


            System.Data.DataTable dt, dt2, dt3;
            dt = ToDataTable<InterfaceVersion>(ss);
            dt2 = ToDataTable<InterfaceMethod>(distinctList2);
            dt3 = ToDataTable<InterfaceRequestField>(distinctList3);

            string filePath = $@"{System.IO.Directory.GetCurrentDirectory()}\swagger.docx";
            try
            {
                CreateFile(filePath);
                //
                //MessageFilter.Register();
                object wdLine = WdUnits.wdLine;
                object oMissing = Missing.Value;
                object fileName = filePath;
                object heading2 = WdBuiltinStyle.wdStyleHeading2;
                object heading3 = WdBuiltinStyle.wdStyleHeading3;

                _Application wordApp = new Application();
                wordApp.Visible = true;
                _Document wordDoc = wordApp.Documents.Open(ref fileName, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                int ii = 0;
                //目录
                foreach (DataRow dr in dt.Rows)
                {
                    string dept = dr["title"].ToString();
                    //string type = dr["Type"].ToString();
                    Paragraph oPara0 = wordDoc.Content.Paragraphs.Add(ref oMissing);
                    oPara0.Range.Text = string.Format("{0}-{1}", ii + 1, dept);
                    //oPara0.Range.Font.Bold = 1;
                    //oPara0.Format.SpaceAfter = 5;
                    oPara0.Range.Select();
                    oPara0.set_Style(ref heading2);
                    oPara0.Range.InsertParagraphAfter();
                    int jj = 0;
                    //子目录
                    foreach (DataRow dr1 in dt2.Rows)
                    {
                        string type = dr1["name"].ToString();
                        string interRoute = dr1["route"].ToString();
                        string ajaxType = dr1["type"].ToString();
                        string controllerName = dr1["name"].ToString();
                        string interDescription = dr1["summary"].ToString();

                        int count = distinctList3.Where(w => w.MethodType == type).Count();
                        int row = count + 5;
                        int column = 5;
                        object ncount = 1;

                        //请求参数table

                        wordApp.Selection.MoveDown(ref wdLine, ref ncount, ref oMissing);
                        wordApp.Selection.TypeParagraph();
                        Paragraph oPara1 = wordDoc.Content.Paragraphs.Add(ref oMissing);
                        oPara1.Range.Select();
                        oPara1.Range.Text = string.Format("{0}-{1}、{2}", ii + 1, jj + 1, interDescription);

                        oPara1.set_Style(ref heading3);
                        oPara1.Range.InsertParagraphAfter();
                        wordApp.Selection.MoveDown(ref wdLine, ref ncount, ref oMissing);
                        wordApp.Selection.TypeParagraph();
                        //设置表格
                        Table table = wordDoc.Tables.Add(wordApp.Selection.Range, row, column, ref oMissing, ref oMissing);

                        table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                        table.Range.Font.Bold = 0;
                        table.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthAuto;
                        table.Columns[1].Width = 120f;
                        table.Columns[2].Width = 60f;
                        table.Columns[3].Width = 150f;
                        table.Columns[4].Width = 60f;
                        table.Columns[5].Width = 60f;
                        //列的合并
                        Cell cell = table.Cell(1, 2);
                        cell.Merge(table.Cell(1, 5));
                        Cell cell2 = table.Cell(2, 2);
                        cell2.Merge(table.Cell(2, 5));
                        Cell cell3 = table.Cell(3, 2);
                        cell3.Merge(table.Cell(3, 5));
                        Cell cell4 = table.Cell(4, 2);
                        cell4.Merge(table.Cell(4, 5));
                        //赋值
                        table.Cell(1, 1).Range.Text = "接口路由：";
                        table.Cell(2, 1).Range.Text = "请求类型：";
                        table.Cell(3, 1).Range.Text = "控制器名称：";
                        table.Cell(4, 1).Range.Text = "接口说明";

                        table.Cell(5, 1).Range.Text = "编号";
                        table.Cell(5, 2).Range.Text = "类型";
                        table.Cell(5, 3).Range.Text = "注解";
                        table.Cell(5, 4).Range.Text = "字段长度";
                        table.Cell(5, 5).Range.Text = "是否必填";

                        table.Cell(1, 2).Range.Text = interRoute;
                        table.Cell(2, 2).Range.Text = ajaxType;
                        table.Cell(3, 2).Range.Text = controllerName;
                        table.Cell(4, 2).Range.Text = interDescription;
                        int kk = 6;
                        //table值
                        foreach (DataRow dr2 in dt3.Rows)
                        {
                            if (dr2["MethodType"].ToString() != type)
                            {
                                continue;
                            }

                            if (dr2["type"].ToString() == "array")
                            {
                                table.Cell(kk, 1).Range.Shading.ForegroundPatternColor = WdColor.wdColorYellow; //背景颜色
                            }
                            //返回实体
                            if (dr2["paramType"].ToString() == "2")
                            {
                                table.Cell(kk, 1).Range.Shading.ForegroundPatternColor = WdColor.wdColorGreen; //背景颜色
                            }
                            table.Cell(kk, 1).Range.Text = dr2["code"].ToString();
                            table.Cell(kk, 2).Range.Text = dr2["type"].ToString();
                            table.Cell(kk, 3).Range.Text = dr2["description"].ToString();
                            table.Cell(kk, 4).Range.Text = dr2["maxLength"].ToString();
                            table.Cell(kk, 5).Range.Text = dr2["isMust"].ToString();
                            kk++;
                        }

                        table.Cell(kk - 1, 5).Range.Select();

                        wordApp.Selection.MoveDown(ref wdLine, ref ncount, ref oMissing);//移动焦点
                        wordApp.Selection.TypeParagraph();//插入段落

                        jj++;
                    }
                    ii++;
                }

                //保存
                wordDoc.Save();
                wordDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                wordApp.Quit(ref oMissing, ref oMissing, ref oMissing);
                //MessageFilter.Revoke();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private static void CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {

                }
            }
        }

        private System.Data.DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new System.Data.DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
    }
}
