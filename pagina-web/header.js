const header_btn = document.querySelector('.header-btn')
const header_menu = document.querySelector(".d-none")
const links = document.querySelector(".links")
header_btn.addEventListener('click',() => {
    if(header_menu.className != 'd-block d-md-flex nav col-12 col-md-auto mb-2 justify-content-center mb-md-0 border-top border-dark'){
    header_menu.className = 'd-block d-md-flex nav col-12 col-md-auto mb-2 justify-content-center mb-md-0 border-top border-dark'
    links.className = 'links d-block d-md-flex col-12 col-md-1 text-end'
}else{
    header_menu.className = 'd-none d-md-flex nav col-12 col-md-auto mb-2 justify-content-center mb-md-0'
    links.className = 'links d-none d-md-flex col-12 col-md-1 text-end'
}
})
$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').trigger('focus')
  })