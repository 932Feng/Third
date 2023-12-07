// See https://aka.ms/new-console-template for more information



// 创建委托实例
NumberChanger nc1 = new NumberChanger(TestDelegate.AddNum);
NumberChanger nc2 = new NumberChanger(TestDelegate.MultNum);
// 使用委托对象调用方法
nc1(25);
Console.WriteLine("Value of Num: {0}", TestDelegate.getNum());
nc2(5);
Console.WriteLine("Value of Num: {0}", TestDelegate.getNum());
Console.ReadKey();




Console.WriteLine("Hello, World!");


delegate int NumberChanger(int n);

public class TestDelegate
{
    static int num = 10;
    public static int AddNum(int p)
    {
        num += p;
        return num;
    }

    public static int MultNum(int q)
    {
        num *= q;
        return num;
    }
    public static int getNum()
    {
        return num;
    }
}
//namespace DelegateAppl
//{
//    class TestDelegate
//    {
//        static int num = 10;
//        public static int AddNum(int p)
//        {
//            num += p;
//            return num;
//        }

//        public static int MultNum(int q)
//        {
//            num *= q;
//            return num;
//        }
//        public static int getNum()
//        {
//            return num;
//        }

//        static void Main(string[] args)
//        {
//            // 创建委托实例
//            NumberChanger nc1 = new NumberChanger(AddNum);
//            NumberChanger nc2 = new NumberChanger(MultNum);
//            // 使用委托对象调用方法
//            nc1(25);
//            Console.WriteLine("Value of Num: {0}", getNum());
//            nc2(5);
//            Console.WriteLine("Value of Num: {0}", getNum());
//            Console.ReadKey();
//        }
//    }
//}