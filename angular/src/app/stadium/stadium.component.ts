import { StadiumFilter } from './../proxy/stadiums/models';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StadiumService, StadiumDto, CreateUpdateStadiumDto } from '@proxy/stadiums';
import { GoogleApiServiceService } from '../api/google-api-service.service';

@Component({
  selector: 'app-stadium',
  templateUrl: './stadium.component.html',
  styleUrls: ['./stadium.component.scss'],
  providers: [ListService],
})
export class StadiumComponent implements OnInit  {
  data = { items: [], totalCount: 0 } as PagedResultDto<StadiumDto>;
  form: FormGroup;
  selected = {} as StadiumDto;
  isModalOpen = false;
  filter: StadiumFilter = {};
  filterHideShow = false;


  options: string[] = ['Apple', 'Banana', 'Orange', 'Mango'];
  filteredOptions: string[] = [];

  filterOptions(input: string): void {
    this.filteredOptions = this.options.filter(option =>
      option.toLowerCase().includes(input.toLowerCase())
    );
  }

  constructor(public readonly list: ListService,
    private service: StadiumService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private googleApiServiceService: GoogleApiServiceService,
    private toast: ToasterService) {}

  ngOnInit() {
    this.getData();
  }

  getData(){
    const streamCreator = (query) => this.service.getListByFilter(query, this.filter);
    this.list.hookToQuery(streamCreator).subscribe((response) => {
      this.data = response;
    });
  }

  toggleFilter(){
    this.filterHideShow = !this.filterHideShow;
  }

  create() {
    this.selected = {} as StadiumDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  edit(id: string) {
    this.service.get(id).subscribe((data) => {
      this.selected = data;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      			displayName: [this.selected.displayName, Validators.required],
			location: [this.selected.location, Validators.required],
			// timeZoneId: [this.selected.timeZoneId, Validators.required],
			// timeZoneDstOffset: [this.selected.timeZoneDstOffset, Validators.required],
			// timeZoneRawOffset: [this.selected.timeZoneRawOffset, Validators.required],

    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    const dto: CreateUpdateStadiumDto = {
      			displayName: this.form.get("displayName").value,
			location: this.form.get("location").value
			// timeZoneId: this.form.get("timeZoneId").value,
			// timeZoneDstOffset: this.form.get("timeZoneDstOffset").value,
			// timeZoneRawOffset: this.form.get("timeZoneRawOffset").value,

    };
    const request = this.selected.id
      ? this.service.update(this.selected.id, dto)
      : this.service.create(dto);

    request.subscribe(() => {
      this.toast.success(this.selected.id?'::StadiumUpdated':'::StadiumCreated', "::SUCCESS", {
        tapToDismiss: true,
        life: 2500
      });
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation.warn('::PressOKToContinue', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.service.delete(id).subscribe(() => {
          this.toast.success('::StadiumDeleted', "::SUCCESS", {
            tapToDismiss: true,
            life: 2500
          });
          this.list.get();
        });
      }
    });
  }
}
