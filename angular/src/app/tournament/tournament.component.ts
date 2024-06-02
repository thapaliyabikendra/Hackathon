import { TournamentFilter } from './../proxy/tournaments/models';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TournamentService, TournamentDto, CreateUpdateTournamentDto } from '@proxy/tournaments';

@Component({
  selector: 'app-tournament',
  templateUrl: './tournament.component.html',
  styleUrls: ['./tournament.component.scss'],
  providers: [ListService],
})
export class TournamentComponent implements OnInit  {
  data = { items: [], totalCount: 0 } as PagedResultDto<TournamentDto>;
  form: FormGroup;
  selected = {} as TournamentDto;
  isModalOpen = false;
  filter: TournamentFilter = {};
  filterHideShow = false;

  constructor(public readonly list: ListService,
    private service: TournamentService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
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
    this.selected = {} as TournamentDto;
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
      			displayName: [this.selected.displayName],
			startDate: [this.selected.startDate],
			endDate: [this.selected.endDate],

    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    const dto: CreateUpdateTournamentDto = {
      			displayName: this.form.get("displayName").value,
			startDate: this.form.get("startDate").value,
			endDate: this.form.get("endDate").value,

    };
    const request = this.selected.id
      ? this.service.update(this.selected.id, dto)
      : this.service.create(dto);

    request.subscribe(() => {
      this.toast.success(this.selected.id?'::TournamentUpdated':'::TournamentCreated', "::SUCCESS", {
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
          this.toast.success('::TournamentDeleted', "::SUCCESS", {
            tapToDismiss: true,
            life: 2500
          });
          this.list.get();
        });
      }
    });
  }
}
