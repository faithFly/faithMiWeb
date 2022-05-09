#获取公约数需要先让a%b一直%到0为止 那么b就是两数的公约数
#这里使用递归算法来实现
def x(a,b):
    #a%b=0就直接返回公约数b
   if a%b == 0:
       return b 
   else:
       #否则递归x函数计算出公约数0为止
       return x(b,a%b)
#输入整数 赋值给a,b
s=input().split()
a=int(s[0]) 
b=int(s[1])
#输出公约数和公倍数公约数就是x(a,b)的结果
#公倍数就是a*b整数除公约数也就是a*b//(x(a,b))
print("{:d} {:d}".format((x(a,b)),a*b//(x(a,b))))
