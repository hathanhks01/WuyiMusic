

function playSong(songPath, ImgUrl, Title) {
    let masterPlay = document.getElementById('masterPlay');
    let wave = document.getElementsByClassName('wave')[0];
    let img = document.getElementById('imgSong');
    let subtitle = document.getElementById('nameSong')
    var audioSource = document.getElementById('audioPlayer');
    audioSource.src = songPath.replace('~', '');
    audioSource.load();
    audioSource.play();
    masterPlay.classList.remove('bi-play-fill');
    masterPlay.classList.add('bi-pause-fill');
    wave.classList.add('active2');
    img.src = ImgUrl;
    subtitle.innerHTML = Title;

}
function playNext() {
    currentIndex = (currentIndex + 1) % songs.length;
    let nextSong = songs[currentIndex];
    playSong(nextSong.FilePath, nextSong.ImgUrl, nextSong.Title);
    console.log('nextSong');
}

function playPrevious() {
    currentIndex = (currentIndex - 1 + songs.length) % songs.length;
    let prevSong = songs[currentIndex];
    playSong(prevSong.FilePath, prevSong.ImgUrl, prevSong.Title);
}

document.addEventListener('DOMContentLoaded', function () {
    let nextButton = document.getElementById('next');
    let backButton = document.getElementById('back');

    if (nextButton && backButton) {
        nextButton.addEventListener('click', playNext);
        backButton.addEventListener('click', playPrevious);
    } else {
        console.error('Các phần tử với ID "next" và "back" không được tìm thấy.');
    }

});