function addToCartjs(event, productId) {
    event.preventDefault();
    const model = {
        Id: productId,
    }
    // console.log("je suis dans le js");
    // console.log(model);

    fetch('/Produit/AddToCart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(model)
    })
        .then(response => {
            // Check if the response is ok and has a body
            if (!response.ok) {
                throw new Error('Failed to add to cart');
            }

            // Try to parse the JSON, if it fails, handle the error
            return response.json().catch(() => {
                throw new Error('Invalid JSON response');
            });
        })
        .then(data => {
            // Check if data is valid
            if (data && data.success) {
                // document.getElementById('cartItemCount').textContent = data.counting;
                window.updateCartCount(data.counting);

            }
            else {
                alert('Produit non ajouté au panier');
            }
        })
}


