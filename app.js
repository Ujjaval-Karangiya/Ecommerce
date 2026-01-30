let products = [];
let debounceTimer;

/* ================= FETCH PRODUCTS ================= */
fetch("https://fakestoreapi.com/products")
  .then(res => res.json())
  .then(data => {
    products = data;
    renderProducts(products);
  })
  .catch(err => console.error(err));

/* ================= RENDER PRODUCTS ================= */
function renderProducts(list) {
  const container = document.getElementById("f2");
  container.innerHTML = "";

  list.forEach(item => {
    container.innerHTML += `
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
  });
}

/* ================= SEARCH (DEBOUNCED) ================= */
function debouncedSearch() {
  clearTimeout(debounceTimer);
  debounceTimer = setTimeout(searchProducts, 300);
}

function searchProducts() {
  const value = document
    .getElementById("searchInput")
    .value
    .toLowerCase()
    .trim();

  const mainList = document.getElementById("f2");
  const searchBox = document.getElementById("productContainer");

  // Empty input → show all products again
  if (!value) {
    searchBox.innerHTML = "";
    mainList.style.display = "flex";
    return;
  }

  mainList.style.display = "none";

  const results = products.filter(p =>
    p.title.toLowerCase().includes(value) ||
    p.category.toLowerCase().includes(value) ||
    p.id.toString().includes(value)
  );

  if (results.length === 0) {
    searchBox.innerHTML = `<p class="text-center text-danger">❌ No products found</p>`;
    return;
  }

  searchBox.innerHTML = results.map(p => `
    <div class="col-md-3 mb-4">
      <div class="card h-100">
        <img src="${p.image}" class="card-img-top p-3" style="height:200px;object-fit:contain">
        <div class="card-body">
          <h6 class="text-truncate">${p.title}</h6>
          <small class="text-muted">${p.category}</small>
          <p class="fw-bold mt-2">$${p.price}</p>
        </div>
      </div>
    </div>
  `).join("");
}

/* ================= CART ================= */
function addToCart(id, title, price, image) {
  let cart = JSON.parse(localStorage.getItem("myCart")) || [];
  let item = cart.find(p => p.id === id);

  if (item) item.qty++;
  else cart.push({ id, title, price, image, qty: 1 });

  localStorage.setItem("myCart", JSON.stringify(cart));
  alert("Added to cart 🛒");
}

const track = document.querySelector(".marquee-track");

if (track) {
    track.innerHTML += track.innerHTML; // duplicate content
}
const userlogin = JSON.parse(localStorage.getItem("CurrentUser"));
document.addEventListener("DOMContentLoaded", () => {userlogin 

    if (!userlogin) {
        window.location.href = "login.html";
        return;
    }

    const nameEls = [
        document.getElementById("userName1"),
        document.getElementById("userName2"),
        document.getElementById("userName3")
    ];

    const emailEls = [
        document.getElementById("email1"),
        document.getElementById("email2"),
        document.getElementById("email3")
    ];

    nameEls.forEach(el => el && (el.textContent = userlogin.name));
    emailEls.forEach(el => el && (el.textContent = userlogin.email));
});

function logout() {
    localStorage.removeItem("CurrentUser");
    alert("Logged out successfully 👋");
    window.location.href = "login.html";
}
function deleteUser() {
    const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));
    const users = JSON.parse(localStorage.getItem("User")) || [];

    if (!currentUser) return alert("No user logged in");

    const deletuser = users.filter(
        user => {
            user.email == "",
                user.name == "",
                    user.password==""}
    );

    localStorage.setItem("User", JSON.stringify(deletuser));
    localStorage.removeItem("CurrentUser");

    alert("Your account has been deleted 🗑️");
    window.location.href = "login.html";
}


function updateuser() {
    let form = document.getElementById("update_js");
    const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));
    const users = JSON.parse(localStorage.getItem("User")) || [];

    if (!currentUser) return;
    form.fname.value.trim() = currentUser.name;
    form.email1.value.trim() = currentUser.email;
    form.fname.password = currentUser.password;
    // remove only logged-in user
    const updateuser = users.filter(
        user => {
            user.name == form.fname.value.trim(),
            user.email == form.email1.value.trim(),
            user.password == form.password.value
        }
    );
    localStorage.setItem("User", JSON.stringify(updateuser));
    localStorage.removeItem("CurrentUser");
    alert("Account updated successfully");
    window.location.href = "login.html";
}