

# region   .Net Framework中的委托与事件
/// <summary>
/// 委托声明原型中的Object类型的参数代表了Subject，也就是监视对象，在本例中是 Heater(热水器)。回调函数(比如Alarm的MakeAlert)可以通过它访问触发事件的对象(Heater)。
///EventArgs 对象包含了Observer所感兴趣的数据，在本例中是temperature。
///上面这些其实不仅仅是为了编码规范而已，这样也使得程序有更大的灵活性。
///比如说，如果我们不光想获得热水器的温度，还想在Observer端(警报器或者显示器)方法中获得它的生产日期、型号、价格，那么委托和方法的声明都会变得很麻烦，
///而如果我们将热水器的引用传给警报器的方法，就可以在方法中直接访问热水器了
/// </summary>
/// 


//.Net Framework中的委托与事件

//尽管上面的范例很好地完成了我们想要完成的工作，但是我们不仅疑惑：为什么.Net Framework 中的事件模型和上面的不同？为什么有很多的EventArgs参数？

//在回答上面的问题之前，我们先搞懂 .Net Framework的编码规范：

//委托类型的名称都应该以EventHandler结束。
//委托的原型定义：有一个void返回值，并接受两个输入参数：一个Object 类型，一个 EventArgs类型(或继承自EventArgs)。
//事件的命名为 委托去掉 EventHandler之后剩余的部分。
//继承自EventArgs的类型应该以EventArgs结尾。
//再做一下说明：

//委托声明原型中的Object类型的参数代表了Subject，也就是监视对象，在本例中是 Heater(热水器)。回调函数(比如Alarm的MakeAlert)可以通过它访问触发事件的对象(Heater)。
//EventArgs 对象包含了Observer所感兴趣的数据，在本例中是temperature。
//上面这些其实不仅仅是为了编码规范而已，这样也使得程序有更大的灵活性。比如说，如果我们不光想获得热水器的温度，还想在Observer端(警报器或者显示器)方法中获得它的生产日期、型号、价格，那么委托和方法的声明都会变得很麻烦，而如果我们将热水器的引用传给警报器的方法，就可以在方法中直接访问热水器了。

//现在我们改写之前的范例，让它符合.Net Framework 的规范：

using System;
using System.Collections.Generic;
using System.Text;

namespace Delegate
{
    // 热水器
    public class Heater
    {
        private int temperature;
        public string type = “RealFire 001”; // 添加型号作为演示
public string area = “China Xian”; // 添加产地作为演示
                                   //声明委托
        public delegate void BoiledEventHandler(Object sender, BoliedEventArgs e);
        public event BoiledEventHandler Boiled; //声明事件

        // 定义BoliedEventArgs类，传递给Observer所感兴趣的信息
        public class BoliedEventArgs : EventArgs
        {
            public readonly int temperature;
            public BoliedEventArgs(int temperature)
            {
                this.temperature = temperature;
            }
        }

        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
        protected virtual void OnBolied(BoliedEventArgs e)
        {
            if (Boiled != null)
            { // 如果有对象注册
                Boiled(this, e); // 调用所有注册对象的方法
            }
        }

        // 烧水。
        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                temperature = i;
                if (temperature > 95)
                {
                    //建立BoliedEventArgs 对象。
                    BoliedEventArgs e = new BoliedEventArgs(temperature);
                    OnBolied(e); // 调用 OnBolied方法
                }
            }
        }
    }

    // 警报器
    public class Alarm
    {
        public void MakeAlert(Object sender, Heater.BoliedEventArgs e)
        {
            Heater heater = (Heater)sender; //这里是不是很熟悉呢？
                                            //访问 sender 中的公共字段
            Console.WriteLine("Alarm：{0} - {1}: ", heater.area, heater.type);
            Console.WriteLine(“Alarm: 嘀嘀嘀，水已经 { 0}
            度了：”, e.temperature);
            Console.WriteLine();
        }

    }

    // 显示器
    public class Display
    {
        public static void ShowMsg(Object sender, Heater.BoliedEventArgs e)
        { //静态方法
            Heater heater = (Heater)sender;
            Console.WriteLine("Display：{0} - {1}: ", heater.area, heater.type);
            Console.WriteLine(“Display：水快烧开了，当前温度：{ 0}
            度。”, e.temperature);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            Heater heater = new Heater();
            Alarm alarm = new Alarm();

            heater.Boiled += alarm.MakeAlert; //注册方法
            heater.Boiled += (new Alarm()).MakeAlert; //给匿名对象注册方法
            heater.Boiled += new Heater.BoiledEventHandler(alarm.MakeAlert); //也可以这么注册
            heater.Boiled += Display.ShowMsg; //注册静态方法

            heater.BoilWater(); //烧水，会自动调用注册过对象的方法
        }
    }
}

输出为：
Alarm：China Xian - RealFire 001:
Alarm: 嘀嘀嘀，水已经 96 度了：
Alarm：China Xian - RealFire 001:
Alarm: 嘀嘀嘀，水已经 96 度了：
Alarm：China Xian - RealFire 001:
Alarm: 嘀嘀嘀，水已经 96 度了：
Display：China Xian - RealFire 001:
Display：水快烧开了，当前温度：96度。
// 省略 …

总结

在本文中我首先通过一个GreetingPeople的小程序向大家介绍了委托的概念、委托用来做什么，随后又引出了事件，接着对委托与事件所产生的中间代码做了粗略的讲述。

在第二个稍微复杂点的热水器的范例中，我向大家简要介绍了 Observer设计模式，并通过实现这个范例完成了该模式，随后讲述了.Net Framework中委托、事件的实现方式。

出处：http://www.cnblogs.com/ziyiFly/archive/2008/09/04/1283913.html

#endregion


#region  下面谈谈我个人对什么文章中所说的委托和事件的理解

///
/// 
///  我们就以自己使用最多的角度来理解上面热水器的事件。需求是这样的，假如报警和显示温度是原来开发出来的功能，
///  客户在使用的过程中A地区的用户要求查看型号和B地区的用户要求查看产地，这些功能只对部分地区用户的要求(例如某个市的用户)，
///  而其他的不同地区的用户或许有不同的要求。那么不同市代理商就要根据本市的要求进行增加不同的模块，那么每个市代理商就只需要关注自己客户要实现的功能。
///  Note：热水器生产商的出厂功能(报警和显示温度)，就好比微软生产的按钮对象，而不同的市代理商可以看作程序员或实施公司
///
///

下面谈谈我个人对什么文章中所说的委托和事件的理解：

真对于上面讲到的热水器来理解委托和事件，感觉理解够透彻，下面我以自己的观点从事件的角度去理解注册事件这个问题。

不管是做web开发还是winForm开发的，我们平常使用最多的我想就是按钮的事件了，如下形式：

protected void btnTest_Click(object sender, EventArgs e)
{//…}

    我们就以自己使用最多的角度来理解上面热水器的事件。需求是这样的，假如报警和显示温度是原来开发出来的功能，客户在使用的过程中A地区的用户要求查看型号和B地区的用户要求查看产地，这些功能只对部分地区用户的要求(例如某个市的用户)，而其他的不同地区的用户或许有不同的要求。那么不同市代理商就要根据本市的要求进行增加不同的模块，那么每个市代理商就只需要关注自己客户要实现的功能。

Note：热水器生产商的出厂功能(报警和显示温度)，就好比微软生产的按钮对象，而不同的市代理商可以看作程序员或实施公司

我们把上面的代码修改下，把热水器类的所有属性字段都放到EventArgs中，对于EventArgs的增加型号和产地字段，如下修改的红色加粗字体：

using System;
    using System.Collections.Generic;
    using System.Text;

namespace Delegate
{
    // 热水器
    public class Heater
    {
        private int temperature;
        public string type = “RealFire 001”; // 添加型号作为演示
public string area = “China Xian”; // 添加产地作为演示
                                   //声明委托
        public delegate void BoiledEventHandler(Object sender, BoliedEventArgs e);
        public event BoiledEventHandler Boiled; //声明事件

        // 定义BoliedEventArgs类，传递给Observer所感兴趣的信息
        public class BoliedEventArgs : EventArgs
        {
            public readonly int temperature;
            public readonly string type;
            public readonly string area;
            public BoliedEventArgs(int temperature, string type, string area)
            {
                this.temperature = temperature;
                this.type = type;
                this.area = area;
            }
        }

        // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
        protected virtual void OnBolied(BoliedEventArgs e)
        {
            if (Boiled != null)
            { // 如果有对象注册
                Boiled(this, e); // 调用所有注册对象的方法
            }
        }

        // 烧水。
        public void BoilWater()
        {
            for (int i = 0; i <= 100; i++)
            {
                temperature = i;
                if (temperature > 95)
                {
                    //建立BoliedEventArgs 对象。
                    BoliedEventArgs e = new BoliedEventArgs(temperature, type, area);
                    OnBolied(e); // 调用 OnBolied方法
                }
            }
        }
    }

    // 警报器
    public class Alarm
    {
        public void MakeAlert(Object sender, Heater.BoliedEventArgs e)
        {
            Heater heater = (Heater)sender; //这里是不是很熟悉呢？
                                            //访问 sender 中的公共字段
            Console.WriteLine("Alarm：{0} - {1}: ", heater.area, heater.type);
            Console.WriteLine(“Alarm: 嘀嘀嘀，水已经 { 0}
            度了：”, e.temperature);
            Console.WriteLine();
        }
    }

    // 显示器
    public class Display
    {
        public static void ShowMsg(Object sender, Heater.BoliedEventArgs e)
        { //静态方法
            Heater heater = (Heater)sender;
            Console.WriteLine("Display：{0} - {1}: ", heater.area, heater.type);
            Console.WriteLine(“Display：水快烧开了，当前温度：{ 0}
            度。”, e.temperature);
            Console.WriteLine();
        }
    }

    // 显示型号
    public class ShowType
    {
        public void show(Object sender, Heater.BoliedEventArgs e)
        {
            Console.WriteLine(“Display：当前的型号：{ 0}”, e.type);
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            Heater heater = new Heater();
            Alarm alarm = new Alarm();

            heater.Boiled += alarm.MakeAlert; //注册方法
            heater.Boiled += (new Alarm()).MakeAlert; //给匿名对象注册方法
            heater.Boiled += new Heater.BoiledEventHandler(alarm.MakeAlert); //也可以这么注册
            heater.Boiled += Display.ShowMsg; //注册静态方法
            heater.Boiled += (new ShowType()).show; //注册显示型号方法

            heater.BoilWater(); //烧水，会自动调用注册过对象的方法
        }
    }
}

上面红色加粗的代码是修改的。上面也只添加了ShowType的方法，如果有城市需要显示产地只需要增加自己的方法，自己的这个方法可以写到Program类中，也可以写到单独的类文件中。只要添加的方法符合事件原型的定义(委托原型定义)就可以把我们自己写的方法注册的该对象上。

看到这里，你是不是有种是曾相似的感觉，不错，在我们写的按钮事件的时候都会有按钮的注册事件，然后我们就能写自己的单击事件的方法的处理代码，例如下面的代码：

btnTest.onClick += btnTest_Click;
#endregion
protected void btnTest_Click(object sender, EventArgs e)
{//…}

    这就是我们平常写的按钮的单击事件的过程。希望后面补充的这部分内容可以从不同的角度视野来更好的帮助我们理解委托和事件。
版权声明：本文为CSDN博主「莫等闲啊」的原创文章，遵循CC 4.0 BY - SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/MDX_BLSNT_KBQ/article/details/122943075


#region  声明一个事件不过类似于声明一个委托类型的变量而已。

    //到了这里，我们不禁想到：面向对象设计，讲究的是对象的封装，既然可以声明委托类型的变量(在上例中是delegate1)，
    //我们何不将这个变量封装到 GreetManager类中？在这个类的客户端中使用不是更方便么？于是，我们改写GreetManager类，像这样：


public class GreetingManager
{
    //在GreetingManager类的内部声明delegate1变量
    public GreetingDelegate delegate1;

    public void GreetPeople(string name, GreetingDelegate MakeGreeting)
    {
        MakeGreeting(name);
    }
}

//现在，我们可以这样使用这个委托变量：


static void Main(string[] args)
{
    GreetingManager gm = new GreetingManager();
    gm.delegate1 = EnglishGreeting;
    gm.delegate1 += ChineseGreeting;

    gm.GreetPeople("Jimmy Zhang", gm.delegate1);
}

//尽管这样达到了我们要的效果，但是似乎并不美气，光是第一个方法注册用“=”，第二个用“+=”就让人觉得别扭。此时，轮到Event出场了，C# 中可以使用事件来专门完成这项工作，我们改写GreetingManager类，它变成了这个样子：


public class GreetingManager
{
    //这一次我们在这里声明一个事件
    public event GreetingDelegate MakeGreet;

    public void GreetPeople(string name, GreetingDelegate MakeGreeting)
    {
        MakeGreeting(name);
    }
}

//很容易注意到：MakeGreet 事件的声明与之前委托变量delegate1的声明唯一的区别是多了一个event关键字。看到这里，你差不多明白到：事件其实没什么不好理解的，声明一个事件不过类似于声明一个委托类型的变量而已。

//我们想当然地改写Main方法：


static void Main(string[] args)
{
    GreetingManager gm = new GreetingManager();
    gm.MakeGreet = EnglishGreeting; // 编译错误1
    gm.MakeGreet += ChineseGreeting;

    gm.GreetPeople("Jimmy Zhang", gm.MakeGreet); //编译错误2
}

//这次，你会得到编译错误：事件“Delegate.GreetingManager.MakeGreet”只能出现在 += 或 -= 的左边(从类型“Delegate.GreetingManager”中使用时除外)。

//事件和委托的编译代码

//这时候，我们不得不注释掉编译错误的行，然后重新进行编译，再借助Reflactor来对 event的声明语句做一探究，看看为什么会发生这样的错误：

//public event GreetingDelegate MakeGreet;



//可以看到，实际上尽管我们在GreetingManager里将 MakeGreet 声明为public，但是，实际上MakeGreet会被编译成 私有字段，难怪会发生上面的编译错误了，因为它根本就不允许在GreetingManager类的外面以赋值的方式访问。
#endregion

