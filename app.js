let products = [];
let debounceTimer;
const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));
if (currentUser == null) {
    window.location.href = "login.html";
}
fetch("https://fakestoreapi.com/products")
    .then(res => res.json())
    .then(data => {
        products = data;
        renderProducts(products);
    })
    .catch(err => console.error(err));

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
            <span class="fs-5 fw-bold text-dark mb-3">₹${item.price}</span>
        </div>
    </div>
</div>

<div class="modal fade" id="modal-${item.id}" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content border-0 shadow-lg rounded-4 overflow-hidden">
            <div class="modal-header border-0"><button type="button" class="btn-close" data-bs-dismiss="modal"></button></div>
            <div class="modal-body p-0">
                <div class="row g-0">
                    <div class="col-md-6 bg-light d-flex align-items-center justify-content-center p-5 rounded-5 ">
                        <img src="${item.image}" class="img-fluid shadow" style="max-height: 350px; object-fit: contain;">
                    </div>
                    <div class="col-md-6 p-4 p-lg-5">
                        <span class="badge bg-secondary-subtle text-secondary mb-2 text-uppercase">${item.category}</span>
                        <h2 class="fw-bold mb-3">${item.title}</h2>
                        <h3 class="text-primary mb-4">₹${item.price}</h3>
                        <p class="text-muted mb-4 small">${item.description}</p>
                         <sapn class="small">⭐ ${item.rating.rate} (${item.rating.count})</sapn>
                         <span id="wishlistbtn" class="btn btn-sm btn-outline-danger rounded-5"
                                onclick="addToWishlist(${item.id}, '${item.title}', ${item.price}, '${item.image}')" data-bs-dismiss="modal">
                                ❤️
                            </span>
                            </div>
                       <div class="d-grid gap-3  p-4 p-lg-5">
                            <button class="btn btn-dark btn-lg rounded-pill"
                                onclick="addToCart(${item.id}, '${item.title}', ${item.price}, '${item.image}')"
                                data-bs-dismiss="modal">
                                🛒 Add to Cart
                            </button>
                        
                          
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


function searchProducts() {
    const input = document.getElementById("searchInput");
    const value = input.value.toLowerCase().trim();
    const modalBody = document.getElementById("searchResultBody");

    if (!value) {
        modalBody.innerHTML = "";
        return;
    }

    const results = products.filter(p =>
        p.title.toLowerCase().includes(value) ||
        p.category.toLowerCase().includes(value)
    );

    modalBody.innerHTML = results.length
        ? results.map(item => `
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
                       <div class="d-grid gap-3">
                             <span class="btn btn-outline-danger btn-lg rounded-pill" data-bs-dismiss="modal"
                                 onclick="addToWishlist(${item.id}, '${item.title}', ${item.price}, '${item.image}')">
                                 ❤️ Add to Wishlist
                              </sapn>

                             <button class="btn btn-dark btn-lg rounded-pill"
                                 onclick="addToCart(${item.id}, '${item.title}', ${item.price}, '${item.image}')"
                                 data-bs-dismiss="modal">
                                 🛒 Add to Cart
                             </button>
                              </div>
                         
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
    `).join("")
        : `<p class="text-danger text-center">No products found</p>`;
}
function debouncedSearch() {
    clearTimeout(debounceTimer);
    debounceTimer = setTimeout(searchProducts, 300);
}

function addToCart(id, title, price, image) {
    const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));

    if (!currentUser) {
        alert("Please login to add items to cart");
        window.location.href = "login.html";
        return;
    }

    const userId = currentUser.id;

    const carts = JSON.parse(localStorage.getItem("userCarts")) || {};

    const cart = carts[userId] || [];

    const item = cart.find(p => p.id === id);

    if (item) {
        item.qty++;
    } else {
        cart.push({ id, title, price, image, qty: 1 });
    }

  
    carts[userId] = cart;
    localStorage.setItem("userCarts", JSON.stringify(carts));
}

function addToWishlist(id, title, price, image) {
    const user = JSON.parse(localStorage.getItem("CurrentUser"));
    const wishlist = JSON.parse(localStorage.getItem("wishlist")) || {};

    if (!user) {
        alert("Please login to add items to wishlist");
        window.location.href = "login.html";
        return;
    }

    const userId = user.id;
    const userWishlist = wishlist[userId] || [];

    const itemExists = userWishlist.some(item => item.id === id);
  
    if (itemExists) {
        wishlist[userId] = userWishlist.filter(item => item.id !== id);
    } else {    
        userWishlist.push({ id, title, price, image });
        wishlist[userId] = userWishlist;
    }
 
    localStorage.setItem("wishlist", JSON.stringify(wishlist));
}


const track = document.querySelector(".marquee-track");

if (track) {
    track.innerHTML += track.innerHTML; // duplicate content
}

function logout() {
    localStorage.removeItem("CurrentUser");
    alert("Logged out successfully 👋");
    window.location.href = "login.html";
}

function deleteUser() {
    const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));
    const users = JSON.parse(localStorage.getItem("User")) || [];

    if (!currentUser) {
        alert("No user logged in");
        return;
    }

    // Remove logged-in user
    const updatedUsers = users.filter(
        user => user.email !== currentUser.email
    );

    localStorage.setItem("User", JSON.stringify(updatedUsers));
    localStorage.removeItem("CurrentUser");

    alert("Your account has been deleted 🗑️");
    window.location.href = "login.html";
}

function updateuser() {
    let form = document.getElementById("update_js");

    const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));
    let users = JSON.parse(localStorage.getItem("User")) || [];
    form.addEventListener("submit", function (e) {
        e.preventDefault();
        if (!currentUser) return;
        users = users.map(user => {
            if (user.email === currentUser.email) {
                return {
                    ...user,
                    name: form.fname.value.trim(),
                    email: form.email1.value.trim(),
                    phone: form.phone.value.trim(),
                    dob: form.dob.value.trim(),
                    address: form.address.value.trim()
                };
            }
            return user;
        });

        console.log(users);
        // Save back to localStorage
        localStorage.setItem("User", JSON.stringify(users));
        localStorage.removeItem("CurrentUser");

        alert("Account updated successfully");
        window.location.href = "login.html";
    });
}

function loadUserData() {
    const form = document.getElementById("update_js");
    const currentUser = JSON.parse(localStorage.getItem("CurrentUser"));
    if (!currentUser) return;
    form.fname.value = currentUser.name;
    form.email1.value = currentUser.email;
    form.phone.value = currentUser.phone;
    form.dob.value = currentUser.dob;
    form.address.value = currentUser.address;
}

function showorders() {
    const user = JSON.parse(localStorage.getItem("CurrentUser"));
    if (!user) {
        alert("Please login first");
        window.location.href = "login.html";
        return;
    }

    const orderlist = JSON.parse(localStorage.getItem("Orders")) || {};
    let order = document.getElementById("order");

    if (!order) {
        console.error("Wish container not found");
        return;
    }



    const orderl = orderlist[user.id] || [];
    order.innerHTML = `<div class="text-center bg-white p-5 rounded shadow-sm">

      <a href="index.html" class="btn btn-dark mt-3">Continue Shopping</a>
</div>`;

    if (orderl) {
        orderl.forEach((item) => {

            order.innerHTML += `
  <div class="row align-items-center bg-white border rounded shadow-sm p-3 mb-3">

    <!-- Image -->
    <div class="col-3 col-md-2 text-center">
      <img src="${item.image}"
           class="img-fluid rounded"
           alt="${item.title}">
    </div>

    <!-- Title + Price -->
    <div class="col-6 col-md-7">
      <h6 class="mb-1 text-truncate">${item.title}</h6>

      <div class="small text-muted">
        Qty: <span class="fw-semibold">${item.qty}</span>
      </div>

      <div class="fw-semibold text-success mt-1">
        ₹${(item.price * item.qty).toFixed(2)}
      </div>

      <div class="small text-muted mt-1">
        Delivered on ${new Date(item.date).toLocaleDateString()}
      </div>
    </div>

    <!-- Status -->
    <div class="col-3 text-end">
      <span class="badge bg-success mb-2 w-100 py-2">
        ✓ Delivered
      </span>

      <button class="btn btn-sm btn-outline-dark w-100" disabled>
        Order Completed
      </button>
    </div>

  </div>
`;

        });
    }
    if (!orders || orders.length === 0) {
        order.innerHTML = `
    <div class="bg-white border rounded shadow-sm p-5 text-center">
      <div class="mb-3 fs-1">📦</div>
      <h5 class="fw-semibold">No orders yet</h5>
      <p class="text-muted mb-4">
        You haven’t placed any orders yet.
      </p>
      <a href="index.html" class="btn btn-dark">
        Start Shopping
      </a>
    </div>
  `;
        return;
    }


};

