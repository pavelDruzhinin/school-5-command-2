<template>
    <div>
        <div class="error" v-if="!v.form.MiddleName.required">Введите поле</div>
        <div class="error" v-if="!v.form.MiddleName.alpha">Допускаются только буквы</div>
    </div>
</template>
<script>
export default {
    props:{
        v:{
            type:Object,
            required:true
        }
    }
}
</script>