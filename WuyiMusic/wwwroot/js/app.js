document.addEventListener('DOMContentLoaded', () => {
    const music = document.getElementById('audioPlayer');
    
    let masterPlay = document.getElementById('masterPlay');
    let wave = document.getElementsByClassName('wave')[0];

    masterPlay.addEventListener('click', () => {
        if (music.paused || music.currentTime <= 0) {            
            music.play();
            masterPlay.classList.remove('bi-play-fill');
            masterPlay.classList.add('bi-pause-fill');
            wave.classList.add('active2');
        } else {
            music.pause();
            masterPlay.classList.remove('bi-pause-fill');
            masterPlay.classList.add('bi-play-fill');
            wave.classList.remove('active2');
        }
    });
    let currentStart = document.getElementById('currentStart');
    let currentEnd = document.getElementById('currentEnd');
    let seek = document.getElementById('seek');
    let bar2 = document.getElementById('bar2');
    let dot = document.getElementsByClassName('dot')[0];

    music.addEventListener('timeupdate', () => {
        let music_curr = music.currentTime;
        let music_dur = music.duration;

        // Tính toán thời gian kết thúc
        let min = Math.floor(music_dur / 60);
        let sec = Math.floor(music_dur % 60);
        if (sec < 10) {
            sec = `0${sec}`;
        }
        currentEnd.innerText = `${min}:${sec}`;

        // Tính toán thời gian bắt đầu
        let min1 = Math.floor(music_curr / 60);
        let sec1 = Math.floor(music_curr % 60);
        if (sec1 < 10) {
            sec1 = `0${sec1}`;
        }
        currentStart.innerText = `${min1}:${sec1}`;

        // Cập nhật thanh seekbar
        let progressbar = parseInt((music.currentTime / music.duration) * 100);
        seek.value = progressbar;
        bar2.style.width = `${progressbar}%`;
        dot.style.left = `${progressbar}%`;
    });

    seek.addEventListener('change', () => {
        music.currentTime = seek.value * music.duration / 100
    })


    let vol_icon = document.getElementById('vol_icon');
    let vol = document.getElementById('vol');
    let vol_dot = document.getElementById('vol_dot');
    let vol_bar = document.getElementsByClassName('vol_bar')[0];

    vol.addEventListener('change', () => {
        if (vol.value == 0) {
            vol_icon.classList.remove('bi-volume-down-fill');
            vol_icon.classList.add('bi-volume-mute-fill');
        } else if (vol.value > 0 && vol.value <= 50) {
            vol_icon.classList.add('bi-volume-down-fill');
            vol_icon.classList.remove('bi-volume-mute-fill');
            vol_icon.classList.remove('bi-volume-up-fill');
        } else if (vol.value > 50) {
            vol_icon.classList.remove('bi-volume-down-fill');
            vol_icon.classList.remove('bi-volume-mute-fill');
            vol_icon.classList.add('bi-volume-up-fill');
        }

        let vol_a = vol.value;
        vol_bar.style.width = `${vol_a}%`;
        vol_dot.style.left = `${vol_a}%`;
        music.volume = vol_a / 100;
    });

    
});
