$(function(){
   
    //获取商品id
    var id = localStorage.getItem('prodId');
    //加载商品各种参数
         $.ajax({
           url: "http://localhost:5001/api/listMain/GetProductByProdId",
           type: "get",
           data:{
             "prodId":id
           },
           contentType: "application/json",
           success: function (res) {
             console.log(res)
               $("#phoneTitle").text(res.item1[0].productName)
               $(".prod_name").text(res.item1[0].productName)
               $(".intro").text(res.item1[0].productIntro)
               $(".pro_price a").text(res.item1[0].productPrice)
               $(".price_sum").text("总计:"+res.item1[0].productPrice+"元")
               $(".price a").text(res.item1[0].productPrice)
               $("#pid").text(res.item1[0].productId)
               //轮播图加图片
               var html="";
               for(let i of res.item2){
                 console.log(1)
                 console.log(i.productPicture1)
                  $('#imgChange').append(`<div><img src="../${i.productPicture1}" height="100%" width="100%"></div>`);
                }
                layui.use('carousel', function(){
                  var carousel = layui.carousel;
                  //建造实例
                  carousel.render({
                    elem: '#rotationMap'
                    ,width: '85%' //设置容器宽度
                    ,height:'100%'
                    ,arrow: 'always' //始终显示箭头
                    //,anim: 'updown' //切换动画方式rotationMap
                  });
                 });
           }
         })
 })
 layui.use('carousel', function(){
 var carousel = layui.carousel;
 //建造实例
 carousel.render({
   elem: '#rotationMap'
   ,width: '85%' //设置容器宽度
   ,height:'100%'
   ,arrow: 'always' //始终显示箭头
   //,anim: 'updown' //切换动画方式rotationMap
 });
});
function addOrder(){
 //获取商品id
 var id = localStorage.getItem('prodId');
 var token=localStorage.getItem('token');
 // 将token设置到请求头headers中
//  $.ajaxSetup({
//   headers:{
//       'Authorization':token
//   }
// })
//获取ajax参数
var UserId=localStorage.getItem('userid');
var ProductId=$("#pid").text();
var data = {
  "userName": UserId,
  "password": ProductId
};
//添加购物车
$.ajax({
  url: "http://localhost:5001/api/Shopping/AddOrder",
  type: "post",
  contentType: "application/json",
  data: JSON.stringify(data),
  success: function (res) {
      console.log(res);
  }, error: function (error) {
      console.log(error)
  }

});
}