// Function to fetch and display products
let url = fetch("https://fakestoreapi.com/products");
  url.then(response => response.json())
  .then(data => {
    const container = document.getElementById("f2");
    container.innerHTML = ""; // Clear loader if any

    data.forEach(item => {
      const card = `
            <div  class="col-sm-6 col-lg-3 p-3 d-flex justify-content-center " >
                <div data-bs-toggle="modal" data-bs-target="#modal-${item.id}" class="card h-100 border-1 overflow-hidden jq " style="width: 20rem; min-height: 450px; border-radius: 20px;">
                    <div class="ratio ratio-4x3 bg-light" >
                        <img src="${item.image}" class="card-img-top p-4" style="object-fit: contain;" alt="${item.title}">
                    </div>
                    <div class="card-body d-flex flex-column p-4">
                    <span class="badge bg-secondary-subtle text-secondary mb-2 text-uppercase">${item.category}</span><br/>
                        <h5 class="card-title text-truncate mb-1">${item.title}</h5>
                        <p class="small">⭐ ${item.rating.rate} (${item.rating.count})</p><hr/>
                        <span class="fs-5 fw-bold text-dark mb-3">$${item.price}</span>
                        <div class="flex-grow-1"></div>
                        <div class="d-flex justify-content-between align-items-center pt-3 border-top">
                            <button class="col-sm-12 btn btn-outline-dark  px-3 rounded-pill" onclick="addToCart(${item.id}, '${item.title}', ${item.price}, '${item.image}')">
                                Add to Cart
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="modal-${item.id}" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered">
                    <div class="modal-content border-0 shadow-lg rounded-4 overflow-hidden">
                        <div class="modal-header border-0"><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div>
                        <div class="modal-body p-0">
                            <div class="row g-0">
                                <div class="col-md-6 bg-light d-flex align-items-center justify-content-center p-5">
                                    <img src="${item.image}" class="img-fluid" style="max-height: 350px; object-fit: contain;">
                                </div>
                                <div class="col-md-6 p-4 p-lg-5">
                                    <span class="badge bg-secondary-subtle text-secondary mb-2 text-uppercase">${item.category}</span>
                                    <h2 class="fw-bold mb-3">${item.title}</h2>
                                    <h3 class="text-primary mb-4">$${item.price}</h3>
                                    <p class="text-muted mb-4 small">${item.description}</p>
                                   <p class="small">⭐ ${item.rating.rate} (${item.rating.count})</p>
                                    <div class="d-grid">
                                        <button class="btn btn-outline-dark btn-lg rounded-pill" onclick="addToCart(${item.id}, '${item.title}', ${item.price}, '${item.image}')">Add to Cart</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>`
            ;
      container.innerHTML += card;
    });
  })
  .catch(error => console.error("Error:", error));



function addToCart(id, title, price, image) {
  let cart = JSON.parse(localStorage.getItem("myCart")) || [];
  let item = cart.find(p => p.id === id);

  if (item) {
    item.qty++;
  } else {
    cart.push({ id, title, price, image, qty: 1 });
  }

  localStorage.setItem("myCart", JSON.stringify(cart));
  alert("Added to cart 🛒");
}

let products = [];

// Fetch API once
url.then(data => {
    products = data;
  });

function searchProducts() {
  const value = document.getElementById("searchInput").value
    .toLowerCase()
    .trim();

  const container = document.getElementById("productContainer");
  container.innerHTML = "";

  if (value === "") {
    container.innerHTML = `
      <p class="text-center text-muted">Start typing to search products 🔍</p>
    `;
    return;
  }

  const results = products.filter(p =>
    p.id.toString().includes(value) ||
    p.title.toLowerCase().includes(value) ||
    p.category.toLowerCase().includes(value)
  );

  if (results.length === 0) {
    container.innerHTML = `
      <p class="text-center text-danger">❌ No matching products</p>
    `;
    return;
  }

  results.forEach(product => {
    container.innerHTML += `
      <div class="col-md-3 mb-4">
        <div class="card h-100 shadow-sm">
          <img src="${product.image}" class="card-img-top p-3" height="200">
          <div class="card-body">
            <h6>${product.title}</h6>
            <p class="text-muted small mb-1">${product.category}</p>
            <p class="fw-bold mb-1">₹ ${product.price}</p>
            <small>⭐ ${product.rating.rate}</small>
          </div>
        </div>
      </div>
    `;
  });
}

