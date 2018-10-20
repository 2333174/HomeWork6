using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;

namespace TestCode
{
    [Serializable]
    public class Order
    {
        public List<OrderDetails> orderDetails = new List<OrderDetails>();
        public string orderNum;
        public string orderName;
        public string orderClient;
        public int tot;
        public Order() { }
        public Order(string num, string name, string client)
        {
            this.orderNum = num;
            this.orderName = name;
            this.orderClient = client;
            tot = 0;
        }
        public void addOrderDatails(OrderDetails b)
        {
            orderDetails.Add(b);
        }
    }
    public class OrderDetails
    {
        public string goodName;
        public string goodPrice;
        public string goodNum;
        public OrderDetails() { }
        public OrderDetails(string name, string price, string number)
        {
            this.goodName = name;
            this.goodNum = number;
            this.goodPrice = price;
        }
    }
    public class OrderService
    {
        int count = 0;
        public List<Order> orderList = new List<Order>();
        public void addOrder(Order b)
        {
            orderList.Add(b);
            count++;
        }

        public void deleteOrder(int i, StreamWriter brout)
        {
            try
            {
                orderList.Remove(orderList[i]);
                count--;
            }
            catch (ArgumentOutOfRangeException)
            {
                brout.WriteLine("该订单不存在");
            }

        }

        public void Import(string xmlFileName)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            FileStream fs = new FileStream(xmlFileName, FileMode.Open);
            orderList = (List<Order>)xmlser.Deserialize(fs);

        }
        public void Export()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Order>));
            string xmlFileName = @"D:\ComputerWork\C#\HomeWork6\HomeWork6\M.xml";
            FileStream fs = new FileStream(xmlFileName, FileMode.Create);
            xmlser.Serialize(fs, orderList);
            fs.Close();
        }
        //查找订单 flag为0按订单号查找，flag为1按订单客人名称 
        public void searchOrder(string s, int flag, StreamWriter brout)
        {
            switch (flag)
            {
                case 0:
                    int t = 0;
                    foreach (Order b in orderList)
                    {
                        if (b.orderNum == s)
                        {
                            t = 1;
                            brout.Write("找到的订单为：");
                            brout.WriteLine(b.orderNum + "  " + b.orderName + "  " + b.orderClient + "  总价：" + b.tot);
                            brout.WriteLine("明细: ");
                            foreach (OrderDetails d in b.orderDetails)
                            {
                                brout.WriteLine(d.goodName + " " + d.goodPrice + " " + d.goodNum);
                            }
                        }
                    }
                    if (t == 0) brout.WriteLine("无此订单");
                    break;
                default:
                    int t1 = 0;
                    foreach (Order b in orderList)
                    {
                        if (b.orderClient == s)
                        {
                            t1 = 1;
                            brout.Write("找到的订单为：");
                            brout.WriteLine(b.orderNum + "  " + b.orderName + "  " + b.orderClient + "  总价：" + b.tot);
                            brout.WriteLine("明细: ");
                            foreach (OrderDetails d in b.orderDetails)
                            {
                                brout.WriteLine(d.goodName + " " + d.goodPrice + " " + d.goodNum);
                            }
                        }
                        if (t1 == 0) brout.WriteLine("无此订单");
                    }
                    brout.WriteLine("无此订单");
                    break;
            }
        }
        //通过Linq查询订单 flag为0按订单号查找，flag为1按订单客人名称，flag为2查询总价超过10000的订单
        public void searchOrderbyLinq(string s, int flag, StreamWriter brout)
        {
            switch (flag)
            {
                case 0:
                    int t = 0;
                    var result = orderList.Where(a => a.orderNum == s);
                    foreach (var b in result)
                    {
                        t = 1;
                        brout.Write("找到的订单为：");
                        brout.WriteLine(b.orderNum + "  " + b.orderName + "  " + b.orderClient + "  总价：" + b.tot);
                        brout.WriteLine("明细: ");
                        foreach (OrderDetails d in b.orderDetails)
                        {
                            brout.WriteLine(d.goodName + " " + d.goodPrice + " " + d.goodNum);
                        }
                    }
                    if (t == 0)
                    {
                        brout.WriteLine("无此订单");
                        break;
                    }
                    break;
                case 1:
                    int t1 = 0;
                    var result1 = orderList.Where(a => a.orderClient == s);
                    foreach (var b in result1)
                    {
                        t1 = 1;
                        brout.Write("找到的订单为：");
                        brout.WriteLine(b.orderNum + "  " + b.orderName + "  " + b.orderClient + "  总价：" + b.tot);
                        brout.WriteLine("明细: ");
                        foreach (OrderDetails d in b.orderDetails)
                        {
                            brout.WriteLine(d.goodName + " " + d.goodPrice + " " + d.goodNum);
                        }
                    }
                    if (t1 == 0)
                    {
                        brout.WriteLine("无此订单");
                        break;
                    }
                    break;
                default:
                    int t2 = 0;
                    var result2 = orderList.Where(a => a.tot > 10000);
                    foreach (var b in result2)
                    {
                        t2 = 1;
                        brout.Write("找到的订单为：");
                        brout.WriteLine(b.orderNum + "  " + b.orderName + "  " + b.orderClient + "  总价：" + b.tot);
                        brout.WriteLine("明细: ");
                        foreach (OrderDetails d in b.orderDetails)
                        {
                            brout.WriteLine(d.goodName + " " + d.goodPrice + " " + d.goodNum);
                        }
                    }
                    if (t2 == 0)
                    {
                        brout.WriteLine("无此订单");
                        break;
                    }
                    break;
            }

        }
        //更改List第num个成员；flag为0，更改订单号，flag为1，更改订单商品名称，flag为2，更改订单客户名称
        public void changeOrder(int num, string s, int flag, StreamWriter brout)
        {
            try
            {
                switch (flag)
                {
                    case 0:
                        orderList[num].orderNum = s;
                        break;
                    case 1:
                        orderList[num].orderName = s;
                        break;
                    default:
                        orderList[num].orderClient = s;
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                brout.WriteLine("需要修改的订单不存在");
            }
        }
    }
    public class enterMain
    {
        public void enterMainby(string pathRead,string pathOut)
        {
            FileStream fin = new FileStream(pathRead, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(pathOut, FileMode.Create, FileAccess.Write);
            StreamReader brin = new StreamReader(fin, System.Text.Encoding.Default);
            StreamWriter brout = new StreamWriter(fout, System.Text.Encoding.Default);
            OrderService c = new OrderService();
            // 是否读取XML文件：Y/N
            string s = brin.ReadLine();
            if (s == "Y")
            {
                //输入文件地址：
                s = brin.ReadLine();
                c.Import(s);

            }
            //输入： 1.添加  2.查找  3.删除  4.修改  #.结束操作
            s = brin.ReadLine();
            string s1, s2, s3, s4, s5, s6;

            while (s != "#")
            {
                int n;
                try
                {
                    n = int.Parse(s);
                }
                catch (Exception e)
                {
                    brout.WriteLine(e.Message);
                    //"输入： 1.添加  2.查找  3.删除  4.修改  #.结束操作"
                    s = brin.ReadLine();
                    continue;
                }
                switch (n)
                {
                    case 1:
                        //读取订单号;
                        s1 = brin.ReadLine();
                        //读取商品种类
                        s2 = brin.ReadLine();
                        //读取客户名称
                        s3 = brin.ReadLine();
                        Order b1 = new Order(s1, s2, s3);
                        //商品条目数量
                        int t = 1;
                        int m = 0;
                        s1 = brin.ReadLine();
                        while (t == 1)
                        {
                            try
                            {

                                m = int.Parse(s1);
                                t = 0;
                            }
                            catch (Exception e)
                            {
                                brout.WriteLine(e.Message);
                                brout.WriteLine("请重新输入商品条目数量：");
                                s1 = brin.ReadLine();
                                continue;
                            }
                        }
                        for (int i = 0; i < m; i++)
                        {
                           //商品名称
                            s4 = brin.ReadLine();
                            //商品单价
                            s5 = brin.ReadLine();
                            t = 1;
                            while (t == 1)
                            {
                                try
                                {

                                    m = int.Parse(s5);
                                    t = 0;
                                }
                                catch (Exception e)
                                {
                                    brout.WriteLine(e.Message);
                                    brout.WriteLine("请重新输入商品单价：");
                                    s5 =brin.ReadLine();
                                    continue;
                                }
                            }
                            //商品数量
                            s6 = brin.ReadLine();
                            t = 1;
                            while (t == 1)
                            {
                                try
                                {

                                    m = int.Parse(s6);
                                    t = 0;
                                }
                                catch (Exception e)
                                {
                                    brout.WriteLine(e.Message);
                                    brout.WriteLine("请重新输入商品数量：");
                                    s6 = brin.ReadLine();
                                    continue;
                                }
                            }
                            OrderDetails a1 = new OrderDetails(s4, s5, s6);
                            b1.addOrderDatails(a1);
                            b1.tot += int.Parse(s5) * int.Parse(s6);
                        }
                        c.addOrder(b1);
                        break;
                    case 2:
                        //查找订单 flag为0按订单号查找，flag为1按订单客人名称，flag为2查询总价超过10000的订单
                        //读取flag
                        int flag = int.Parse(brin.ReadLine());
                        if (flag != 2)
                        {
                            //读取查找内容
                            //c.searchOrder(Console.ReadLine(), flag);
                            c.searchOrderbyLinq(brin.ReadLine(), flag,brout);
                        }
                        else
                        {
                            c.searchOrderbyLinq("", flag,brout);
                        }
                        break;
                    case 3:
                        //删除第n个元素：
                        int n1 = int.Parse(brin.ReadLine());
                        c.deleteOrder(n1,brout);
                        break;
                    case 4:
                        //更改List第num个成员；flag为0，更改订单号，flag为1，更改订单商品名称，flag为2，更改订单客户名称
                        //读取num：
                        int num = int.Parse(brin.ReadLine());
                        //读取flag
                        int flag1 = int.Parse(brin.ReadLine());
                        //修改为
                        c.changeOrder(num, brin.ReadLine(), flag1,brout);
                        break;


                }
                if (n != 2)
                {
                    brout.Write("当前订单为：");
                    foreach (Order b in c.orderList)
                    {
                        brout.WriteLine(b.orderNum + "  " + b.orderName + "  " + b.orderClient + "  总价：" + b.tot);
                        brout.WriteLine("明细: ");
                        foreach (OrderDetails d in b.orderDetails)
                        {
                            brout.WriteLine(d.goodName + " " + d.goodPrice + " " + d.goodNum);
                        }
                    }
                    brout.WriteLine();
                }
                //输入： 1.添加  2.查找  3.删除  4.修改  #.结束操作
                s = brin.ReadLine();
            }
            //是否写入文件：Y/N
            s = brin.ReadLine();
            if (s == "Y")
            {
                c.Export();
                string xml = File.ReadAllText(@"D:\ComputerWork\C#\HomeWork6\HomeWork6\M.xml");
                brout.WriteLine(xml);
            }
            brout.Close();
            brin.Close();
        }

    }
    class Program
    {
       
        static void Main(string[] args)
        {
            enterMain Em = new enterMain();
            Em.enterMainby(@"D:\ComputerWork\C#\HomeWork6\TestTXT\True.txt", @"D:\ComputerWork\C#\HomeWork6\TestTXT\outTrue.txt");
            Em.enterMainby(@"D:\ComputerWork\C#\HomeWork6\TestTXT\False.txt", @"D:\ComputerWork\C#\HomeWork6\TestTXT\outFalse.txt");
        }
    }
}

