// ================================
// GLOBAL CART COUNT
// ================================
function setCartCount(count) {
    $("#cartCount").text(count);
}

// ================================
// TOAST MESSAGE
// ================================
function showToast(msg) {
    let toast = $("#toast");

    if (toast.length === 0) {
        $("body").append(`<div id="toast" class="toast-msg"></div>`);
        toast = $("#toast");
    }

    toast.text(msg).fadeIn(200);

    setTimeout(function () {
        toast.fadeOut(400);
    }, 2000);
}

// ================================
// ADD TO CART (HOME + PRODUCTS)
// ================================
$(document).on("click", ".btn-add-cart", function () {

    const id = $(this).data("id");

    $.post("/Cart/AddAjax", { id: id }, function (res) {

        // ❌ Not logged in
        if (res.login === false) {
            showToast("⚠️ Please login to add items to cart");
            setTimeout(() => {
                window.location.href = "/Auth/Login";
            }, 1200);
            return;
        }

        // ❌ Failed
        if (!res.success) {
            showToast("❌ Failed to add product");
            return;
        }

        // ✅ Success
        setCartCount(res.count);
        showToast("✅ Added to cart");
    });
});

// ================================
// LIVE SEARCH (AUTO SUGGEST)
// ================================
$(document).ready(function () {

    $("#searchBox").on("keyup", function () {

        const q = $(this).val();

        if (q.length < 1) {
            $("#suggestions").empty();
            return;
        }

        $.get("/Search/Suggest", { q: q }, function (data) {

            const list = $("#suggestions");
            list.empty();

            if (!data || data.length === 0) return;

            data.forEach(p => {
                list.append(`
                    <li class="list-group-item suggestion-item"
                        data-id="${p.id}">
                        ${p.name} - ${p.price} ৳
                    </li>
                `);
            });
        });
    });

    $(document).on("click", ".suggestion-item", function () {
        const id = $(this).data("id");
        window.location.href = "/Product/Details/" + id;
    });
});
