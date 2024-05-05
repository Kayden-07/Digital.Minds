function loadFooter() {
    fetch('FooterB.html')
      .then(response => response.text())
      .then(data => {
        // Encuentra el elemento donde se mostrará el header
        const footerContainer = document.getElementById('Footer-container');
        // Inserta el contenido del header en el elemento
        footerContainer.innerHTML = data;
      })
      .catch(error => {
        console.error('Error al cargar el Footer:', error);
      });
  }