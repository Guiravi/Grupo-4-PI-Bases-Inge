let likeBtn = document.getElementById('like');
let sameBtn = document.getElementById('same');
let dislikeBtn = document.getElementById('dislike');
let likeTxt = document.getElementById('textLike');
let sameTxt = document.getElementById('textSame');
let dislikeTxt = document.getElementById('textDislike');

likeBtn.addEventListener('mouseover', showLikeText);
sameBtn.addEventListener('mouseover', showSameText);
dislikeBtn.addEventListener('mouseover', showDislikeText);
likeBtn.addEventListener('mouseout', hideLikeText);
sameBtn.addEventListener('mouseout', hideSameText);
dislikeBtn.addEventListener('mouseout', hideDislikeText);


function showLikeText() {
    likeTxt.style.visibility = 'visible';
}

function showSameText() {
    sameTxt.style.visibility = 'visible';
}

function showDislikeText() {
    dislikeTxt.style.visibility = 'visible';
}

function hideLikeText() {
    likeTxt.style.visibility = 'hidden';
}

function hideSameText() {
    sameTxt.style.visibility = 'hidden';
}

function hideDislikeText() {
    dislikeTxt.style.visibility = 'hidden';
}