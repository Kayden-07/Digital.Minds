function loadHeader() {
    fetch('headerB.html')
      .then(response => response.text())
      .then(data => {
        // Encuentra el elemento donde se mostrará el header
        const headerContainer = document.getElementById('header-container');
        // Inserta el contenido del header en el elemento
        headerContainer.innerHTML = data;
      })
      .catch(error => {
        console.error('Error al cargar el header:', error);
      });
  }