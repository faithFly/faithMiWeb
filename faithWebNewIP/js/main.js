$(function() {   
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
      // 将token设置到请求头headers中
      // $.ajaxSetup({
      //     headers:{
      //         'Authorization':token
      //     }
      // })
      //加载手机页面
      $.ajax({
        url: "http://112.124.10.13:5001/api/listMain/GetProductByCateName",
        type: "get",
        data:"cateName=手机",
        success: function (res) {
         for(let i of res){
             $('#phone_dv').append(` <li>
              <img src="../${i.productPicture}" alt="">
              <a style="display:none;">${i.productId}</a>
              <h3>${i.productName}</h3>
              <p>${i.productTitle}</p>
              <span>${i.productPrice}元起</span>
            </li>`)
           }
           $('#phone_dv').append(` <li>
             <p style="line-height:300px;">浏览更多</p>
            </li>`)
        }, error: function (error) {
            console.log(error)
        }

    });
        //电视机
        $.ajax({
        url: "http://112.124.10.13:5001/api/listMain/GetProductByCateName",
        type: "get",
        data:"cateName=充电器",
        success: function (res) {
         for(let i of res){
             $('#tv_dv').append(` <li>
              <img src="../${i.productPicture}" alt="">
              <a style="display:none;">${i.productId}</a>
              <h3>${i.productName}</h3>
              <p>${i.productTitle}</p>
              <span>${i.productPrice}元起</span>
            </li>`)
           }
           $('#tv_dv').append(` <li>
             <p style="line-height:300px;">浏览更多</p>
            </li>`)
        }, error: function (error) {
            console.log(error)
        }

    });

     //配件
     $.ajax({
        url: "http://112.124.10.13:5001/api/listMain/GetProductByCateName",
        type: "get",
        data:"cateName=电视机",
        success: function (res) {
         for(let i of res){
             $('#list_dv').append(` <li>
              <img src="../${i.productPicture}" alt="">
              <a style="display:none;">${i.productId}</a>
              <h3>${i.productName}</h3>
              <p>${i.productTitle}</p>
              <span>${i.productPrice}元起</span>
            </li>`)
           }
           $('#list_dv').append(` <li>
             <p style="line-height:300px;">浏览更多</p>
            </li>`)
        }, error: function (error) {
            console.log(error)
        }

    });
     
    //点击购物车跳转
    $(".icon_txt").click(function(){
      var uid = localStorage.getItem('userid');
      if(uid==null||uid==""){
        layer.msg("还没有登录呢！"); 
        return;
      }
      window.location.href="shoppingCart.html";

    })
   
})
layui.use('carousel', function(){
var carousel = layui.carousel;
//建造实例
carousel.render({
elem: '#test1',
height:'600px'
,width: '70%' //设置容器宽度
,arrow: 'hover' //始终显示箭头
//,anim: 'updown' //切换动画方式
});
});