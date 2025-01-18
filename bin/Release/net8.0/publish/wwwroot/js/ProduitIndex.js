const forms = document.querySelectorAll('.form-product');

forms.forEach(form => {
    form.addEventListener('click', function (event) {
        event.preventDefault();

        const idInput = form.querySelector('.id'); // Sélectionnez l'input spécifique au formulaire
        const model = {
            Id: idInput.value,
        };

        fetch('/Produit/ProduitDetails', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(model)
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    console.log(data.produit);
                    // Redirigez vers une page spécifique si nécessaire
                    window.location.href = `/Produit/GetProduit/${data.produit.id}`;
                }
            })
            .catch(error => console.error('Erreur :', error));
    });
});
