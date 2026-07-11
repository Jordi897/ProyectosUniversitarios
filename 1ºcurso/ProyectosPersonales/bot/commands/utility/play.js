const {createAudioResource, NoSubscriberBehavior, createAudioPlayer, getVoiceConnection, AudioPlayerStatus, joinVoiceChannel } = require('@discordjs/voice');
const { SlashCommandBuilder, VoiceChannel, MessageFlags } = require('discord.js');

module.exports = {
    data: new SlashCommandBuilder()
        .setName('play')
        .setDescription('Pon la musica que quieras reproducir')
        .addStringOption(option => option
            .setName('url')
            .setDescription('URL')
            .setRequired(true)
        ),
    async execute(interaction) {
        interaction.reply({content: 'No funciona adecuadamente',  flags: MessageFlags.Ephemeral})
        const member = interaction.member
                if(member.voice.channel){
                        const conection = joinVoiceChannel({
                                channelId: member.voice.channelId,
                                guildId: interaction.guildId,
                                adapterCreator: interaction.guild.voiceAdapterCreator
                        });
                };
                const conection = getVoiceConnection(interaction.guild.id);
                const player = createAudioPlayer();
                let resouce = createAudioResource(`./Musicabot/Rocky Balboa - Música de entrenamiento [ADGFuE7T8Qc].mp3`);
                player.play(resouce);
                conection.subscribe(player);
    },
};