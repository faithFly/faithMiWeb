$(function (){
      // 获取token  
      var token = localStorage.getItem('token');
      var userName=localStorage.getItem('userName');
      //如果有token证明已经登录
      if(token!=""){
      //登录成功后实现的特效隐藏前两个a标签
      $("#topbar_changeA a").eq(0).css("display","none");
      $("#topbar_changeA a").eq(1).css("display","none");
      $("#topbar_changeA a").eq(2).html(userName);
      $("#topbar_changeA span").eq(0).css("display","none");

      }
    //获取userId
    var userId = localStorage.getItem('userid');
    layui.use('table', function(){
        var table = layui.table;
        var url='http://localhost:5001/api/Shopping/GetShoppingcart?userId='+userId;
        //第一个实例
        table.render({
          id:'idTest',
          elem: '#demo'
          ,height: 650
          ,url:url  //数据接口
          ,parseData: function(res){ //res 即为原始返回的数据
            return {
              "code": res.code, //解析接口状态
              "msg": res.msg, //解析提示文本
              "count": res.count, //解析数据长度
              "data": res.data //解析数据列表
            };
          },totalRow:true
          ,cols: [[ //表头
             {type:'checkbox',totalRowText:'合计'}
            ,{field: 'id', title: 'ID', width:80}
            ,{field: 'product_pic', title: '图片', width:150,height:80,templet:function(data){
              return '<img src="../'+data.product_pic+'" width=50 height=50>'
            }}
            ,{field: 'productName', title: '商品名称', width:300}
            ,{field: 'productPrice', title: '商品单价', width:150} 
            ,{field: 'cartNum', title: '数量', width:300,align: 'center'} 
            ,{field: 'productTot', title: '总计', width:150,totalRow:true,align: 'center'} 
            ,{field: 'fileName',fixed: 'right',title:'操作',align: 'center'
            ,templet:function (d) { //这里的参数d是templete的固定用法，可以取到该行的所有数据
                if(d.fileName ==null){ //未上传
                    //class里的upload_btn是用来标志上传按钮的，没有定义实际的css样式
                    return '<button onclick="fun1('+d.id+')" class="layui-btn layui-btn-sm upload_btn" value="'+d.id+'" >删除</button>';
                }
            }}
          ]]
        });
        
      });
      //结算按钮
      $("#ord_Btn").click(function(){
        //获取userid
        var userId = localStorage.getItem('userid');
        layui.use('table', function(){
          var table = layui.table;
          var checkStatus = table.checkStatus('idTest'); //idTest 即为基础参数 id 对应的值
          var list = [];
          if(checkStatus.data.length<1){
            layer.msg("请至少选择一样商品");
            return; 
          }
            
            checkStatus.data.forEach(item => {
           var arr={}
           arr.ProductId=item.productId;
           arr.UserId=userId;
           arr.Money=item.productPrice*item.cartNum;
           arr.BuyNum=item.cartNum;
           list.push(arr);
          });
         //将token设置到请求头headers中
          $.ajaxSetup({
            headers:{
                'Authorization':"Bearer "+token
            }
          })
                //ajax生成订单
                $.ajax({
                  url: "http://localhost:5001/api/Order/AddOrderProduct",
                  type: "post",
                  contentType: 'application/json; charset=UTF-8',
                  data: JSON.stringify(list),
                  dataType : "json",
                  success: function (res) {
                      if(res.regCode==0){
                        console.log(res)
                        layer.msg(res.regMsg); 
                        localStorage.setItem("orderId",res.orderId); 
                        window.location.href="Order.html";
                      }else{
                        layer.msg(res.regMsg); 
                      }
                  }, error: function (error) {
                       // layer.msg(res.regMsg); 
                       console.log(error);
                  }

                });
        })
       
      });
});
function fun1(d){
  var UserId=localStorage.getItem('userid');
  var token=localStorage.getItem('token');
  //将token设置到请求头headers中
  $.ajaxSetup({
   headers:{
       'Authorization':"Bearer "+token
   }
 })
  //获取json
  var data={
    sid:d,
    userId:UserId
  }
  //添加购物车
  $.ajax({
    url: "http://localhost:5001/api/Shopping/DelShopping",
    type: "post",
    contentType: "application/json",
    data: JSON.stringify(data),
    success: function (res) {
        if(res.regCode==0){
          layer.msg(res.regMsg); 
          location.reload();
        }else{
          layer.msg(res.regMsg); 
        }
    }, error: function (error) {
          layer.msg(res.regMsg); 
    }

  });
}


