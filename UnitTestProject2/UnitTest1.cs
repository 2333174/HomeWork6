using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HomeWork6;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Order b1 = new Order("201701", "食品", "张三");
            OrderDetails b10 = new OrderDetails("薯片", "2", "1");
            b1.addOrderDatails(b10);
            Order b2 = new Order("201702", "饮品", "李四");
            OrderService c = new OrderService();
            c.addOrder(b1);
            c.addOrder(b2);
            List<Order> orderList = new List<Order>();
            orderList.Add(b1);
            orderList.Add(b2);
            CollectionAssert.AreEqual(c.orderList, orderList);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Order b1 = new Order("201701", "食品", "张三");
            OrderDetails b10 = new OrderDetails("薯片", "2", "1");
            b1.addOrderDatails(b10);
            Order b2 = new Order("201702", "饮品", "李四");
            OrderService c = new OrderService();
            c.addOrder(b1);
            c.addOrder(b2);
            List<Order> orderList = new List<Order>();
            orderList.Add(b1);
            orderList.Add(b2);
            c.deleteOrder(1);
            orderList.Remove(orderList[1]);
            CollectionAssert.AreEqual(orderList, c.orderList,"不相等");
            Assert.AreEqual(c.deleteOrder(1), false);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Order b1 = new Order("201701", "食品", "张三");
            OrderDetails b10 = new OrderDetails("薯片", "2", "1");
            b1.addOrderDatails(b10);
            Order b2 = new Order("201702", "饮品", "李四");
            OrderDetails b20 = new OrderDetails("薯片", "2", "10000");
            b2.addOrderDatails(b20);
            OrderService c = new OrderService();
            c.addOrder(b1);
            c.addOrder(b2);
            List<Order> orderList = new List<Order>();
            orderList.Add(b1);
            orderList.Add(b2);
            Assert.AreEqual(orderList[0], c.searchOrderbyLinq("201701",0), "不相等");
            Assert.AreEqual(orderList[0], c.searchOrderbyLinq("张三", 1), "不相等");
            Assert.AreEqual(orderList[1], c.searchOrderbyLinq("1000", 2), "不相等");
        }
        [TestMethod]
        public void TestMethod4()
        {
            Order b1 = new Order("201701", "食品", "张三");
            OrderService c = new OrderService();
            c.addOrder(b1);
            c.changeOrder(0, "201702", 0);
            Assert.AreEqual(c.orderList[0].orderNum, "201702");
            c.changeOrder(0, "饮品", 1);
            Assert.AreEqual(c.orderList[0].orderName, "饮品");
            c.changeOrder(0, "李四", 2);
            Assert.AreEqual(c.orderList[0].orderClient, "李四");
            Assert.AreEqual(c.changeOrder(1, "饮品", 0), false, " 需要修改的订单不存在");
        }
    }
}
