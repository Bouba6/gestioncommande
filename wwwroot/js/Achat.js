function increaseQuantity(id) {
    const input = document.getElementById(`quantity_${id}`);
    let value = parseInt(input.value) || 0;
    input.value = value + 1;
    updateQuantityInSession(id, value + 1);
}

function decreaseQuantity(id) {
    console.log("je cherche l'id");
    console.log(id);
    const input = document.getElementById(`quantity_${id}`);
    let value = parseInt(input.value) || 0;
    if (value > 1) {
        input.value = value - 1;
        updateQuantityInSession(id, value - 1);
    }
}


function updateQuantityInSession(id, quantity) {
    const model = {
        Id: id,
        qteCommande: quantity
    }
    console.log(model);
    fetch('/Achat/UpdateQuantityInSession', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(model)
    })
        .then(response => {
            // Check if the response is ok and has a body
            if (!response.ok) {
                throw new Error('Failed to update quantity');
            }

            // Try to parse the JSON, if it fails, handle the error
            return response.json().catch(() => {
                throw new Error('Invalid JSON response');
            });
        })
        .then(data => {

            if (data && data.success) {
                const totalElement = document.getElementById('total');
                totalElement.style.display = 'block';

                if (data.value == 1) {
                    totalElement.innerHTML = `
                        <div class="flex items-center gap-3">
                            <span class="line-through text-gray-400">${data.newTotal.toFixed(2)} $</span>
                            <span class="text-green-600 font-bold">${(data.newTotal * 0.9).toFixed(2)} $</span>
                            <span class="bg-green-100 text-green-800 text-xs font-medium px-2.5 py-0.5 rounded">-10%</span>
                        </div>`;
                } else {
                    totalElement.innerText = `Sous Total : ${data.newTotal.toFixed(2)} $`;
                }
            } else {
                console.error('Error updating quantity', data);
            }
        })
        .catch(error => {
            console.error('Error updating quantity:', error);
        });
}



function deletedetail(event, id) {
    alert("je cherche l'id");
    // event.preventDefault();
    // console.log(id);
    // fetch(`/Achat/Deletedetail/${id}`, {
    //     method: 'POST'
    // })
    //     .then(() => window.location.reload());
}












document.addEventListener('DOMContentLoaded', function () {
    fetch('/Achat/UpdateQuantityInSession', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ Id: 0, qteCommande: 0 }) // Valeurs par défaut
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                document.getElementById('total').style.display = 'block';
                document.getElementById('total').innerText = `Sous Total : ${data.value == 1 ? (data.newTotal * 0.9).toFixed(2) : data.newTotal.toFixed(2)} $`;
            }
        });
});

// const form = Document.getElementById('form')

// form.addEventListener('submit', function (event) {

// })