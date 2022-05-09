$(function (){
    // 获取token  
    var token = localStorage.getItem('token');
    var userName=localStorage.getItem('userName');
     //将token设置到请求头headers中
        $.ajaxSetup({
            headers:{
                'Authorization':"Bearer "+token
            }
        })
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
  var orderId = localStorage.getItem('orderId');
  var data={
    "orderId":orderId,
    "userId":userId
  }
    //加载商品各种参数
    $.ajax({
        url: "http://localhost:5001/api/Order/SelectOrderProduct",
        type: "post",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (res) {
        var html="";
        for(let i of res.item1){
          html +=` <li>
          <img src="../${i.prodPic}" width="50" height="50"> 
          <a class="a_prodName">${i.prodName}</a>
          <a class="a_priceNum">${i.prodPrice}×${i.prodNum}</a>
          <a class="a_price">${i.prodSum}</a>
        </li>`
        }
        $('#orderList').html(html);
        console.log(res.item2);
        $("#price_txt").text(res.item2);
        $("#sum_txt").text(res.item2);
        },error: function (error) {
            layer.msg(res.regMsg); 
      }
    })
    //结算
    $("#end_Btn").click(function(){
       // 获取token  
    var token = localStorage.getItem('token');
    var userName=localStorage.getItem('userName');
     //将token设置到请求头headers中
        $.ajaxSetup({
            headers:{
                'Authorization':"Bearer "+token
            }
        })
     //获取userId
      var userId = localStorage.getItem('userid');
      var orderId = localStorage.getItem('orderId');
      var data={
        "orderId":orderId,
        "userId":userId
      }
       //加载商品各种参数
            $.ajax({
              url: "http://localhost:5001/api/Order/SettlementOrder",
              type: "post",
              data: JSON.stringify(data),
              contentType: "application/json",
              success: function (res) {
                layer.msg("结算成功！"); 
                window.location.href="main.html";
              },error: function (error) {
                  layer.msg(res.regMsg); 
            }
          })
    });
      
 });