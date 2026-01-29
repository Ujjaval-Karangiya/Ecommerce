fetch("https://fakestoreapi.com/products")
    .then(response => response.json())
    .then(data => {
        const container = document.getElementById("f2");

        data.forEach(item => {
            const card = `
                              <div class="col-sm-6 col-lg-3 p-3 d-flex justify-content-center">
      <div class="card h-100 border-1 overflow-hidden jq" style="width: 20rem; min-height: 450px;">

        <div class="ratio ratio-4x3 bg-light">
          <img src="${item.image}" class="card-img-top p-4 rounder-pill shadow-lg" style="object-fit: contain;" alt="${item.title}">
        </div>

        <div class="card-body d-flex flex-column p-4 rounder-pill shadow-lg">
          <h5 class="card-title text-truncate mb-1">${item.title}</h5>
            <span class="fs-5 fw-bold text-dark">$${item.price}</span>
          <div class="flex-grow-1"></div>

          <div class="d-flex justify-content-between align-items-center pt-3 border-top">
            <button class="btn btn-dark btn-sm px-3 rounded-pill" data-bs-toggle="modal"
data-bs-target="#modal-${item.id}">view detail</button>
           <a href="cart.html">
              <button class="btn btn-dark btn-sm px-3 rounded-pill"onclick="addToCart('${item.id}', '${item.title}', '${item.price}', '${item.image}')">Add to cart</button>
           </a>
          </div>
        </div>
      </div>
</div>



<div class="modal fade" id="modal-${item.id}" tabindex="-1" aria-hidden="true">
      <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content border-0 shadow-lg">

          <div class="modal-header border-0">
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>

          <div class="modal-body p-0">
            <div class="row g-0">

              <div class="col-md-6 bg-light d-flex align-items-center justify-content-center p-5 rounder-pill shadow-lg">
                <img src="${item.image}" class="img-fluid" style="max-height: 350px; object-fit: contain;" alt="${item.title}">
              </div>

              <div class="col-md-6 p-4 p-lg-5">
                <span class="badge bg-secondary-subtle text-secondary mb-2 text-uppercase fw-bold">${item.category}</span>
                <h2 class="fw-bold mb-3">${item.title}</h2>
                <h3 class="text-primary mb-4">$${item.price}</h3>

                <h6 class="fw-bold text-uppercase small text-muted">Description</h6>
                <p class="text-muted mb-4">
                  ${item.description}
                </p>

                <div class="d-grid">
                 <a href="cart.html">
                  <button class="btn btn-outline-dark btn-lg rounded-pill shadow-sm">
   <i class="fa-solid fa-cart-shopping me-2" onclick="addToCart('${item.id}', '${item.title}', '${item.price}', '${item.image}')"></i>Add to Cart
 </button>
                 </a>
                </div>
              </div>

            </div>
          </div>

        </div>
      </div>
</div>
                  `;
            container.innerHTML += card;
        });
    })
    .catch(error => console.error("Error:", error));

// Store cart items in an array
//let cart = [];

//function addToCart(id, title, price, image) {
//    // 1. Add item to array
//    cart.push({ id, title, price, image });

//    // 2. Update the UI
//    renderCart();

//    // 3. Optional: Open the sidebar automatically when adding
//    const cartSidebar = new bootstrap.Offcanvas(document.getElementById('canva'));
//    cartSidebar.show();
//}

//function renderCart() {
//    const container = document.getElementById('cart-items-container');
//    const subtotalEl = document.getElementById('cart-subtotal');
//    const emptyMsg = document.getElementById('empty-cart-msg');

//    if (cart.length === 0) {
//        emptyMsg.classList.remove('d-none');
//        container.innerHTML = '';
//        subtotalEl.innerText = `$0.00`;
//        return;
//    }

//    emptyMsg.classList.add('d-none');
//    container.innerHTML = cart.map((item, index) => `
//        <div class="d-flex align-items-center mb-3 p-2 bg-secondary bg-opacity-10 rounded-3">
//            <img src="${item.image}" class="rounded" style="width: 50px; height: 50px; object-fit: cover;">
//            <div class="ms-3 flex-grow-1">
//                <h6 class="mb-0 small text-truncate" style="max-width: 150px;">${item.title}</h6>
//                <span class="small text-primary fw-bold">$${item.price}</span>
//            </div>
//            <button class="btn btn-sm text-danger" onclick="removeItem(${index})">
//                <i class="fa-solid fa-trash"></i>
//            </button>
//        </div>
//    `).join('');

//    const total = cart.reduce((sum, item) => sum + parseFloat(item.price), 0);
//    subtotalEl.innerText = `$${total.toFixed(2)}`;
//}

//function removeItem(index) {
//    cart.splice(index, 1);
//    renderCart();
//}

//function clearCart() {
//    cart = [];
//    renderCart();
//}

// ===== CART STORAGE =====
function addToCart(id, title, price, image) {
    // get current cart
    const cart = JSON.parse(localStorage.getItem("cart")) || [];

    // add new item
    cart.push({ id, title, price, image });

    // save back to localStorage
    localStorage.setItem("cart", JSON.stringify(cart));

    alert("Added to cart");
}
